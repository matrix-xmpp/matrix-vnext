using Shouldly;
using Matrix.Core.Crypt;
using Xunit;

namespace Matrix.Core.Tests.Crypt
{
    public class UnitTest1
    {
        [Fact]
        public void HashAlgorithmsFromNameTest()
        {
            Hash.HashAlgorithmsFromName("sha-384").ShouldBe(HashAlgorithms.Sha384);
            Hash.HashAlgorithmsFromName("SHa-384").ShouldBe(HashAlgorithms.Sha384);
            Hash.HashAlgorithmsFromName("sha-385").ShouldNotBe(HashAlgorithms.Sha384);
            Hash.HashAlgorithmsFromName("sha-222").ShouldBe(HashAlgorithms.Unknown);
            Hash.HashAlgorithmsFromName("foo").ShouldBe(HashAlgorithms.Unknown);
            Hash.HashAlgorithmsFromName(null).ShouldBe(HashAlgorithms.Unknown);
        }

        /*
            Test vectors from: http://www.di-mgt.com.au/sha_testvectors.html
            
            Input message: "abc", the bit string (0x)616263 of length 24 bits.
            
            Algorithm Output
            ================
            SHA-1	    a9993e36 4706816a ba3e2571 7850c26c 9cd0d89d
            SHA-224	    23097d22 3405d822 8642a477 bda255b3 2aadbce4 bda0b3f7 e36c9da7
            SHA-256	    ba7816bf 8f01cfea 414140de 5dae2223 b00361a3 96177a9c b410ff61 f20015ad
            SHA-384	    cb00753f45a35e8b b5a03d699ac65007 272c32ab0eded163 1a8b605a43ff5bed 8086072ba1e7cc23 58baeca134c825a7
            SHA-512	    ddaf35a193617aba cc417349ae204131 12e6fa4e89a97ea2 0a9eeee64b55d39a 2192992a274fc1a8 36ba3c23a3feebbd 454d4423643ce80e 2a9ac94fa54ca49f
            SHA-3-224	e642824c3f8cf24a d09234ee7d3c766f c9a3a5168d0c94ad 73b46fdf
            SHA-3-256	3a985da74fe225b2 045c172d6bd390bd 855f086e3e9d525b 46bfe24511431532
            SHA-3-384	ec01498288516fc9 26459f58e2c6ad8d f9b473cb0fc08c25 96da7cf0e49be4b2 98d88cea927ac7f5 39f1edf228376d25
            SHA-3-512	b751850b1a57168a 5693cd924b6b096e 08f621827444f70d 884f5d0240d2712e 10e116e9192af3c9 1a7ec57647e39340 57340b4cf408d5a5 6592f8274eec53f0

            MD5         900150983CD24FB0D6963F7D28E17F72
        */


        [Fact]
        public void HashAlgorithmsOutputTest()
        {
            Hash.Sha1HashHex("abc").ShouldBe("a9993e364706816aba3e25717850c26c9cd0d89d");
            Hash.Sha256HashHex("abc").ShouldBe("ba7816bf8f01cfea414140de5dae2223b00361a396177a9cb410ff61f20015ad");
            Hash.Md5HashHex("abc").ToUpper() .ShouldBe("900150983CD24FB0D6963F7D28E17F72");
        }

    }
}
