# High-Performance Log Analytics & Telemetry Suite (HPLATS)

A high-speed Windows desktop application that simulates a real-world enterprise monitoring tool. It parses massive log files in C, processes data via C++ engine, and displays the results in an interactive C# WinForms dashboard.

## About This Project
This is my first public hobby project, built as a personal showcase rather than a general-purpose tool. The end goal is to create a fast, reliable, and easy-to-use program for reading large log files. I work on this in my free time whenever inspiration strikes.
The log files are dumb and doesn't represent anything. Those are randomly generated and will be reworked for better simulation.

## Tech Stack
* **Parser:** C (high-speed raw file parsing)
* **Backend Engine:** C++ (DLL processing engine)
* **Frontend GUI:** C# / .NET 9 (Windows Froms)

## Roadmap (To-Do)
* **Realistic Log Formats:** Transition to more realistic log structures (such as large binary dumps).
* **UI Overhaul:** Redesign the GUI to be cleaner and more user-friendly.
* **Documentation:** Add thorough code comments throughout the repository.

## Getting Started (Building from source)
### Prerequisites
* Windows 10/11
* Visual Studio 2022 (with **Desktop development with C++** and **.NET desktop development** workloads)
* .NET 9.0 SDK

### Build Steps
1. Clone the repository:
   ```bash
   git clone https://github.com/Kiksumiks/HPLATS.git
   ```
2. Open **`HPLATS.sln`** in Visual Studio.
3. In the top toolbar, set the Solution Configuration to **Debug** (or **Release**) and the Solution Platform to **x64**.
4. Press **Ctrl + Shift + B** to build the solution (this automatically compiles the C++ DLL and copies it to the GUI output folder).
5. Press **F5** to launch the application and load one of the sample logs from the `samples/` folder.

## Feedback

I would love to hear your thoughts, suggestions, or feedback on this project! Feel free to open an issue or reach out.