# EmDbg

This is a very work in progress project, it is not ready for general usage just yet.

EmDbg is a work-in-progress debugging client/library for modded Xbox 360 consoles, using the [NeighborSharp](https://github.com/InvoxiPlayGames/NeighborSharp) library, written in C# .NET 6.0.

A WinForms-based GUI frontend is included, and a Dear ImGui-based GUI frontend is in the works.

## Features

- Searching for and connecting to XBDM instances
- Debug print messages
- Exception handling and skipping (WinForms only)
- Disassembly view (*partial*) (WinForms only)

## TODO

- Stability improvements
    - Better reboot/reconnect handling
	- Thread safety
	- Improve memory usage
- Memory viewer/editor (*backend implemented*)
- Breakpoint manager (*backend implemented*)
- Module list
- Thread list
- Exception/breakpoint state dumping
- Register viewer (for threads and exceptions) (*backend implemented*)
- More modular backend to support other non-Xbox PPC platforms in the future (Wii)
- Better GUI/backend separation, and non-WinForms cross-platform GUI (working on it with ImGui)

## Building

1. Clone the repo via `git clone https://github.com/InvoxiPlayGames/EmDbg.git --recursive`
2. Open the solution file in Visual Studio (2022 ideally, but 2019 with the .NET 6.0 SDK should work)
3. Build/run the project

Dependencies should be automatically downloaded via NuGet.

## License

EmDbg is licensed under the GNU Lesser General Public License 3.0, or any later version at your own choice. See LICENSE.txt for more information.

### Third-Party

- This project makes use of [Dear ImGui](https://github.com/ocornut/imgui), licensed under the MIT license.
- This project makes use of [ImGui.NET](https://github.com/mellinoe/ImGui.NET), licensed under the MIT license.
- This project makes use of [Veldrid](https://github.com/mellinoe/veldrid), licensed under the MIT license.
- This project makes use of [Capstone](https://github.com/capstone-engine/capstone), licensed under the BSD 3-clause license.
- This project makes use of [Capstone.NET](https://github.com/9ee1/Capstone.NET), licensed under the MIT license.
