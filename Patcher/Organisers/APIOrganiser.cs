﻿using System;
using Mono.Cecil;
using System.Linq;

namespace OTA.Patcher.Organisers
{
    public class APIOrganiser : Organiser
    {
        public APIOrganiser(AssemblyDefinition assembly) : base(assembly)
        {
        }

        #region "Callbacks"

        public TypeDefinition BasePlayer
        {
            get
            { return _asm.MainModule.Types.Single(x => x.Name == "BasePlayer"); }
        }

        public TypeDefinition ChestCallback
        {
            get
            { return _asm.MainModule.Types.Single(x => x.Name == "ChestCallback"); }
        }

        public TypeDefinition CollisionCallback
        {
            get
            { return _asm.MainModule.Types.Single(x => x.Name == "CollisionCallback"); }
        }

        public TypeDefinition Configuration
        {
            get
            { return _asm.MainModule.Types.Single(x => x.Name == "Configuration" && x.Namespace == "OTA.Callbacks"); }
        }

        public TypeDefinition GameWindow
        {
            get
            { return _asm.MainModule.Types.Single(x => x.Name == "GameWindow"); }
        }

        public TypeDefinition IAPISocket
        {
            get
            { return _asm.MainModule.Types.Single(x => x.Name == "IAPISocket"); }
        }

        public TypeDefinition INativeMod
        {
            get
            { return _asm.MainModule.Types.Single(x => x.Name == "INativeMod"); }
        }

#if CLIENT
        public TypeDefinition InterfaceCallback
        {
            get
            { return _asm.MainModule.Types.Single(x => x.Name == "InterfaceCallback"); }
        }
#endif

        public TypeDefinition ItemCallback
        {
            get
            { return _asm.MainModule.Types.Single(x => x.Name == "ItemCallback"); }
        }

        public TypeDefinition MainCallback
        {
            get
            { return _asm.MainModule.Types.Single(x => x.Name == "MainCallback"); }
        }

        public TypeDefinition MapCallback
        {
            get
            { return _asm.MainModule.Types.Single(x => x.Name == "MapCallback"); }
        }

        public TypeDefinition MessageBufferCallback
        {
            get
            { return _asm.MainModule.Types.Single(x => x.Name == "MessageBufferCallback"); }
        }

        public TypeDefinition NetMessageCallback
        {
            get
            { return _asm.MainModule.Types.Single(x => x.Name == "NetMessageCallback"); }
        }

        public TypeDefinition NetplayCallback
        {
            get
            { return _asm.MainModule.Types.Single(x => x.Name == "NetplayCallback"); }
        }

        public TypeDefinition NPCCallback
        {
            get
            { return _asm.MainModule.Types.Single(x => x.Name == "NPCCallback"); }
        }

        public TypeDefinition Patches
        {
            get
            { return _asm.MainModule.Types.Single(x => x.Name == "Patches"); }
        }

        public TypeDefinition ProjectileCallback
        {
            get
            { return _asm.MainModule.Types.Single(x => x.Name == "ProjectileCallback"); }
        }

        public TypeDefinition Rand
        {
            get
            { return _asm.MainModule.Types.Single(x => x.Name == "Rand"); }
        }

        public TypeDefinition RemoteClientCallback
        {
            get
            { return _asm.MainModule.Types.Single(x => x.Name == "RemoteClientCallback"); }
        }

        public TypeDefinition Sender
        {
            get
            { return _asm.MainModule.Types.Single(x => x.Name == "Sender"); }
        }

        public TypeDefinition Tools
        {
            get
            { return _asm.MainModule.Types.Single(x => x.Name == "Tools"); }
        }

        public TypeDefinition Logger
        {
            get
            { return _asm.MainModule.Types.Single(x => x.Name == "Logger"); }
        }

        public TypeDefinition WorldFileCallback
        {
            get
            { return _asm.MainModule.Types.Single(x => x.Name == "WorldFileCallback"); }
        }

        public TypeDefinition VanillaHooks
        {
            get
            { return _asm.MainModule.Types.Single(x => x.Name == "VanillaHooks"); }
        }

        public TypeDefinition WorldGenCallback
        {
            get
            { return _asm.MainModule.Types.Single(x => x.Name == "WorldGenCallback"); }
        }

        #endregion

        #region Client Registers

        //        #if CLIENT
        public TypeDefinition NpcModRegister
        {
            get
            { return _asm.MainModule.Types.Single(x => x.Name == "NpcModRegister"); }
        }

        public TypeDefinition TileModRegister
        {
            get
            { return _asm.MainModule.Types.Single(x => x.Name == "TileModRegister"); }
        }
        //        #endif

        #endregion

        public TypeDefinition Connection
        {
            get
            { return _asm.MainModule.Types.Single(x => x.Name == "Connection"); }
        }

        public TypeDefinition ClientConnection
        {
            get
#if !CUSTOM_SOCKETS
            { return _asm.MainModule.Types.Single(x => x.Name == "TemporarySynchSock"); }
#else
            { return _asm.MainModule.Types.Single(x => x.Name == "ClientConnection"); }
#endif
        }

        public TypeDefinition Player
        {
            get
            { return _asm.MainModule.Types.Single(x => x.Name == "PlayerCallback"); }
        }

        public TypeDefinition MemTile
        {
            get
            { return _asm.MainModule.Types.Single(x => x.Name == "MemTile"); }
        }

        public TypeDefinition NAT
        {
            get
            { return _asm.MainModule.Types.Single(x => x.Name == "NAT"); }
        }

        public TypeDefinition TileCollection
        {
            get
            { return _asm.MainModule.Types.Single(x => x.Name == "TileCollection"); }
        }

        public TypeDefinition Utilities
        {
            get
            { return _asm.MainModule.Types.Single(x => x.Name == "Utilities"); }
        }

        public TypeDefinition WorldSender
        {
            get
            { return _asm.MainModule.Types.Single(x => x.Name == "WorldSender" && x.IsAbstract); }
        }
    }
}

