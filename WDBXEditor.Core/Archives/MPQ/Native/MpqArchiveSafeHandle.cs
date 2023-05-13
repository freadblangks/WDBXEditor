﻿using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace WDBXEditor.Core.Archives.MPQ.Native
{
	internal sealed class MpqArchiveSafeHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		public MpqArchiveSafeHandle(IntPtr handle)
			: base(true)
		{
			SetHandle(handle);
		}

		public MpqArchiveSafeHandle()
			: base(true) { }

		protected override bool ReleaseHandle()
		{
			return NativeMethods.SFileCloseArchive(handle);
		}
	}
}