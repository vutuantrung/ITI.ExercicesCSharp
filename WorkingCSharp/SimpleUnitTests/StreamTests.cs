using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace SimpleUnitTests
{
    [TestFixture]
    public class StreamTests
    {
        static string ThisFilePath([CallerFilePath]string p = null) => p;

        [Test]
        public void copying_stream_to_stream()
        {
            var origin = ThisFilePath();
            var target = origin + ".bak";

            CopyFile(origin, target);

            File.ReadAllBytes(origin)
                .SequenceEqual(File.ReadAllBytes(target))
                .Should().BeTrue();

            if (File.Exists(target))
            {
                File.Delete(target);
            }
        }

        void CopyFile(string origin, string target)
        {
            using (FileStream fs_r = new FileStream(origin, FileMode.Open, FileAccess.Read, FileShare.None))
            using (FileStream fs_w = new FileStream(target, FileMode.Append, FileAccess.Write, FileShare.None))
            {
                byte[] b = new byte[1024];
                int len;
                do
                {
                    len = fs_r.Read(b, 0, b.Length);
                    fs_w.Write(b, 0, len);
                } while (len == b.Length);
            }
        }
    }
}
