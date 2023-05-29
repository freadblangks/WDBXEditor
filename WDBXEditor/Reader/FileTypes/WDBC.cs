using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acmil.Storage;

namespace Acmil.Reader.FileTypes
{
    public class WDBC : DBHeader
    {
        public override void ReadHeader(ref BinaryReader dbReader, string signature)
        {
            base.ReadHeader(ref dbReader, signature);
        }
    }
}
