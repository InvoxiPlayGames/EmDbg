namespace EmDbg.ImGuiUI;

public class MemoryAccess
{
    public static XboxDebugger currentDebugger;
    public const uint BLOCK_SIZE = 0x80;
    public static Semaphore concurrentBlocks = new(4, 4);
    
    public class MemoryBlock
    {
        public enum Status
        {
            UNLOADED,
            REQUESTED,
            LOADED,
            ERROR
        }

        public Status status = Status.UNLOADED;
        private byte[] content;
        public uint offset;

        public byte[]? GetContent()
        {
            switch (status)
            {
                case Status.UNLOADED:
                    Request();
                    return null;
                case Status.REQUESTED:
                    return null;
                case Status.LOADED:
                    return content;
                case Status.ERROR:
                    return null;
            }

            return null;
        }

        public uint GlobalAddressToBlockContentIndex(uint address)
        {
            return address - offset;
        } 

        public void Request()
        {
            if (status == Status.UNLOADED)
            {
                status = Status.REQUESTED;
                Load();
            }
        }

        private async void Load()
        {
            var task = Task.Run(() =>
            {
                try
                {
                    concurrentBlocks.WaitOne();
                    Console.WriteLine("Loading memory block at " + offset.ToString("X8"));
                    content = currentDebugger.GetMemory(offset, BLOCK_SIZE);
                    status = Status.LOADED;
                    concurrentBlocks.Release();
                } catch (Exception e)
                {
                    Console.WriteLine("Failed to load memory block at " + offset.ToString("X8") + ": " + e.Message);
                    status = Status.ERROR;
                    concurrentBlocks.Release();
                }
                
            });

            try
            {
                await task;
            }
            catch (Exception e)
            {
                status = Status.ERROR;
            }
            
        }

        public MemoryBlock(uint offset)
        {
            this.offset = offset;
        }
    }
    
    public static Dictionary<uint, MemoryBlock> memoryBlocks = new();
    
    public static uint AddressToBlockIndex(uint address)
    {
        return address / BLOCK_SIZE;
    }

    public static uint BlockIndexToAddress(uint blockIndex)
    {
        return blockIndex * BLOCK_SIZE;
    }

    public static MemoryBlock GetMemoryBlock(uint blockIndex)
    {
        if (memoryBlocks.TryGetValue(blockIndex, out var block))
        {
            return block;
        }
        else
        {
            memoryBlocks[blockIndex] = new MemoryBlock(BlockIndexToAddress(blockIndex));
            return  memoryBlocks[blockIndex];
        }
    }

    public static MemoryBlock[] GetBlocksForAddressRange(uint start, uint end)
    {
        uint startBlock = AddressToBlockIndex(start);
        uint endBlock = AddressToBlockIndex(end);

        uint blockCount = endBlock - startBlock + 1;
        MemoryBlock[] blocks = new MemoryBlock[blockCount];
        for (uint i = 0; i < blockCount; i++)
        {
            blocks[i] = GetMemoryBlock(startBlock + i);
        }

        return blocks;
    }

    public static void FlushMemoryAtRange(uint start, uint end)
    {
        var blocks =  GetBlocksForAddressRange(start, end);
        foreach (var block in blocks)
        {
            block.status = MemoryBlock.Status.UNLOADED;
        }
    }

    // Will return null if any of the blocks in the range are not loaded yet
    public static byte[]? GetMemory(uint address, uint size)
    {
        var blocks = GetBlocksForAddressRange(address, address + size);
        var unloaded = false;
        foreach (var block in blocks)
        {
            if (block.status != MemoryBlock.Status.LOADED)
            {
                unloaded = true;
            }
            block.Request();
        }

        if (unloaded)
        {
            return null;
        }
        
        byte[] data = new byte[size];

        uint localBlockIndex = 0;
        for (uint i = 0; i < size; i++)
        {
            var globalAddr = i + address;
            while (blocks[localBlockIndex].GlobalAddressToBlockContentIndex(globalAddr) >= BLOCK_SIZE) 
            {
                localBlockIndex += 1;
            }

            var content = blocks[localBlockIndex].GetContent();
            data[i] = content[blocks[localBlockIndex].GlobalAddressToBlockContentIndex(globalAddr)];
        }

        return data;
    }

    public static uint? GetWord(uint address)
    {
        var val = GetMemory(address, 4);
        if (val != null)
        {
            return BitConverter.ToUInt32(val);
        }
        return null;
    }

    public static byte? GetByte(uint address)
    {
        var val = GetMemory(address, 1);
        return val?[0];
    }

    // Only call if you know the value at the address is a string.
    public static string? ReadU8String(uint address)
    {
        uint addressEnd = address;
        for (int i = 0; i < 255; i++)
        {
            var read = GetByte(addressEnd);
            if (read == null)
            {
                return null;
            }

            if (read == 0)
            {
                break;
            }

            addressEnd++;
        }

        var fullStringData = GetMemory(address, addressEnd - address);
        // Sanity check, shouldn't occur if the null terminator scan never returned null
        if (fullStringData == null)
        {
            return null;
        }
        
        return System.Text.Encoding.UTF8.GetString(fullStringData);
    }
    
    public static string? GuessString(uint address)
    {
        // If the first 3 chars at address are printable ASCII we'll go ahead and attempt to read it as a string
        // This does mean it won't detect strings that are shorter but oh well
        for (uint i = 0; i < 3; i++)
        {
            var read = GetByte(address+i);
            if (read == null)
            {
                return null;
            }

            if (!(read >= 0x20 && read <= 0x7E))
            {
                return null;
            }
        }

        return ReadU8String(address);
    }
}