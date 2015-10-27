﻿using System;
using System.IO;
using OTA.Plugin;
using System.Diagnostics;
using OTA.Logging;
using OTA.Command;


#if Full_API
using Terraria;
using Terraria.Initializers;
using Terraria.IO;
#endif

namespace OTA.Callbacks
{
    /// <summary>
    /// The configuration callback from vanilla code
    /// </summary>
    public static class Configuration
    {
        /// <summary>
        /// Command line configuration callback
        /// </summary>
        /// <param name="game">Game.</param>
        public static void StartupConfig(Microsoft.Xna.Framework.Game game)
        {
            #if Full_API
            if (Tools.RuntimePlatform != RuntimePlatform.Mono)
            {
                try
                {
                    string s;
                    if ((s = LaunchInitializer.TryParameter(new string[]
                        {
                            "-forcepriority"
                        })) != null)
                    {
                        Process currentProcess = Process.GetCurrentProcess();
                        int num;
                        if (int.TryParse(s, out num))
                        {
                            switch (num)
                            {
                                case 0:
                                    currentProcess.PriorityClass = ProcessPriorityClass.RealTime;
                                    break;
                                case 1:
                                    currentProcess.PriorityClass = ProcessPriorityClass.High;
                                    break;
                                case 2:
                                    currentProcess.PriorityClass = ProcessPriorityClass.AboveNormal;
                                    break;
                                case 3:
                                    currentProcess.PriorityClass = ProcessPriorityClass.Normal;
                                    break;
                                case 4:
                                    currentProcess.PriorityClass = ProcessPriorityClass.BelowNormal;
                                    break;
                                case 5:
                                    currentProcess.PriorityClass = ProcessPriorityClass.Idle;
                                    break;
                                default:
                                    currentProcess.PriorityClass = ProcessPriorityClass.High;
                                    break;
                            }
                        }
                        else
                        {
                            currentProcess.PriorityClass = ProcessPriorityClass.High;
                        }
                    }
                    else
                    {
                        Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
                    }
                }
                catch
                {
                }
            }
            #endif

            var ctx = new HookContext()
            {
                Sender = HookContext.ConsoleSender
            };
            var args = new HookArgs.ParseCommandLineArguments();
            HookPoints.ParseCommandLineArguments.Invoke(ref ctx, ref args);
        }

        /// <summary>
        /// Loads a terraria server configuration file.
        /// </summary>
        /// <remarks>>
        /// OTA callback instead of vanilla code loading it, this way we have full control over the input 
        /// </remarks>
        /// <param name="file">File.</param>
        public static void Load(string file)
        {
            ConfigUpdater.SourceFile = file;
            if (File.Exists(file))
                using (var sr = new StreamReader(file))
                {
                    string line;
                    while (!sr.EndOfStream)
                    {
                        line = sr.ReadLine();
                        if (line != null)
                        {
                            var ix = line.IndexOf("=");
                            if (ix > -1)
                            {
                                var key = line.Substring(0, ix).Trim();
                                var value = line.Substring(ix + 1, line.Length - (ix + 1));
#if Full_API
                                switch (key.ToLower())
                                {
                                    case "world":
                                        Main.ActiveWorldFileData = WorldFile.GetAllMetadata(value, false);
                                        break;
                                    case "port":
                                        int port;
                                        if (!Int32.TryParse(value, out port))
                                        {
                                            ProgramLog.Log("Failed to parse config option {0}", key);
                                        }
                                        else
                                            Terraria.Netplay.ListenPort = port;
                                        break;
                                    case "maxplayers":
                                        int maxplayers;
                                        if (!Int32.TryParse(value, out maxplayers))
                                        {
                                            ProgramLog.Log("Failed to parse config option {0}", key);
                                        }
                                        else
                                            Terraria.Main.maxNetPlayers = maxplayers;
                                        break;
                                    case "priority":
                                        if (!Program.LaunchParameters.ContainsKey("-forcepriority"))
                                        {
                                            if (Tools.RuntimePlatform != RuntimePlatform.Mono)
                                            {
                                                try
                                                {
                                                    int priority = Convert.ToInt32(value);
                                                    if (priority >= 0 && priority <= 5)
                                                    {
                                                        Process currentProcess = Process.GetCurrentProcess();
                                                        if (priority == 0)
                                                        {
                                                            currentProcess.PriorityClass = ProcessPriorityClass.RealTime;
                                                        }
                                                        else if (priority == 1)
                                                        {
                                                            currentProcess.PriorityClass = ProcessPriorityClass.High;
                                                        }
                                                        else if (priority == 2)
                                                        {
                                                            currentProcess.PriorityClass = ProcessPriorityClass.AboveNormal;
                                                        }
                                                        else if (priority == 3)
                                                        {
                                                            currentProcess.PriorityClass = ProcessPriorityClass.Normal;
                                                        }
                                                        else if (priority == 4)
                                                        {
                                                            currentProcess.PriorityClass = ProcessPriorityClass.BelowNormal;
                                                        }
                                                        else if (priority == 5)
                                                        {
                                                            currentProcess.PriorityClass = ProcessPriorityClass.Idle;
                                                        }
                                                    }
                                                    else ProgramLog.Log("Invalid priority value: " + priority);
                                                }
                                                catch
                                                {
                                                }
                                            }
                                            else
                                                ProgramLog.Log("Skipped setting process priority on mono");
                                        }
                                        break;
                                    case "password":
                                        Terraria.Netplay.ServerPassword = value;
                                        break;
                                    case "motd":
                                        Terraria.Main.motd = value;
                                        break;
                                    case "lang":
                                        int lang;
                                        if (!Int32.TryParse(value, out lang))
                                        {
                                            ProgramLog.Log("Failed to parse config option {0}", key);
                                        }
                                        else
                                            Lang.lang = lang;
                                        break;
                                    case "worldpath":
                                        Terraria.Main.WorldPath = value;
                                        break;
                                    case "worldname":
                                        Terraria.Main.worldName = value;
                                        break;
                                    case "banlist":
                                        Netplay.BanFilePath = value;
                                        break;
                                    case "difficulty":
                                        Main.expertMode = value == "1";
                                        break;
                                    case "autocreate":
                                        int autocreate;
                                        if (!Int32.TryParse(value, out autocreate))
                                        {
                                            ProgramLog.Log("Failed to parse config option {0}", key);
                                        }
                                        else
                                        {
                                            switch (autocreate)
                                            {
                                                case 0:
                                                    Terraria.Main.autoGen = false;
                                                    break;
                                                case 1:
                                                    Terraria.Main.maxTilesX = 4200;
                                                    Terraria.Main.maxTilesY = 1200;
                                                    Terraria.Main.autoGen = true;
                                                    break;
                                                case 2:
                                                    Terraria.Main.maxTilesX = 6300;
                                                    Terraria.Main.maxTilesY = 1800;
                                                    Terraria.Main.autoGen = true;
                                                    break;
                                                case 3:
                                                    Terraria.Main.maxTilesX = 8400;
                                                    Terraria.Main.maxTilesY = 2400;
                                                    Terraria.Main.autoGen = true;
                                                    break;
                                            }
                                        }
                                        break;
                                    case "secure":
                                        Terraria.Netplay.spamCheck = value == "1";
                                        break;
                                    case "upnp":
                                        Terraria.Netplay.UseUPNP = value == "1";
                                        if (Terraria.Netplay.UseUPNP && Tools.RuntimePlatform == RuntimePlatform.Mono)
                                        {
                                            ProgramLog.Log("[Warning] uPNP is only available on Windows platforms.");
                                        }
                                        break;
                                    case "npcstream":
                                        int npcstream;
                                        if (!Int32.TryParse(value, out npcstream))
                                        {
                                            ProgramLog.Log("Failed to parse config option {0}", key);
                                        }
                                        else
                                            Terraria.Main.npcStreamSpeed = npcstream;
                                        break;
                                    default:
                                        var ctx = new HookContext()
                                        {
                                            Sender = HookContext.ConsoleSender
                                        };
                                        var args = new HookArgs.ConfigurationFileLineRead()
                                        {
                                            Key = key,
                                            Value = value
                                        };

                                        HookPoints.ConfigurationFileLineRead.Invoke(ref ctx, ref args);
                                        break;
                                }
#endif
                            }
                        }
                        else
                            break;
                    }
                }
            else
                ProgramLog.Log($"Configuration `{file}` was specified but does not exist.");
        }
    }
}
