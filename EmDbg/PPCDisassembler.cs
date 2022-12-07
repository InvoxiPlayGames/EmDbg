using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gee.External.Capstone;
using Gee.External.Capstone.PowerPc;

namespace EmDbg
{
    public class PPCDisassembler
    {
        public static string[] DisassembleToStrings(byte[] data, uint startAddress, bool is64bit)
        {
            // set capstone disassembler mode based on 64-bit toggle
            PowerPcDisassembleMode mode = PowerPcDisassembleMode.BigEndian;
            if (is64bit)
                mode |= PowerPcDisassembleMode.Bit64;
            else
                mode |= PowerPcDisassembleMode.Bit32;
            // make sure we have valid PPC instruction alignment
            if (data.Length % 4 != 0)
                throw new Exception("Disassembler recieved data that is not a multiple of 4.");
            int numInstructions = data.Length / 4;
            // create a lookup table of addr:instruction for later use (only instructions are returned to caller)
            Dictionary<long, string> disassembly = new();
            // loop through all instructions and set unknown values for later, in case
            for (uint i = startAddress; i < startAddress + data.Length; i+=4)
            {
                int arrayStart = (int)(i - startAddress);
                disassembly[i] = $"unk: {BitConverter.ToString(data, arrayStart, 4)}";
            }
            // run binary data through capstone to get actual instructions
            // TODO: capstone stops after running into 1 invalid inst, detect this happening and skip the invalid instruction
            // TODO 2: capstone might not understand some instructions available on the wii/360 CPUs so we have to parse those manually
            using (CapstonePowerPcDisassembler disassembler = CapstoneDisassembler.CreatePowerPcDisassembler(mode))
            {
                disassembler.EnableInstructionDetails = true;
                PowerPcInstruction[] instructions = disassembler.Disassemble(data, startAddress);
                foreach(PowerPcInstruction instruction in instructions)
                {
                    disassembly[instruction.Address] = instruction.Mnemonic + " " + instruction.Operand;
                }
            }
            // return the value as a string array, its the caller's job to remember its own base address.
            return disassembly.Values.ToArray();
        }
    }
}
