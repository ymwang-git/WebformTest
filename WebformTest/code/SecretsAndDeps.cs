using System;

namespace WebformTest.code
{
    /// <summary>
    /// 代刚ノ贺祑絪絏/ゅ盞絛ㄒ度ㄑ scanner 代刚
    /// </summary>
    public static class SecretsAndDeps
    {
        // 安 AWS 芲ノ secrets 盎代代刚
        public const string AwsAccessKeyId = "AKIAEXAMPLEFAKEACCESSKEY";
        public const string AwsSecretAccessKey = "wJalrXUtnFEMI/K7MDENG/bPxRfiCYEXAMPLEKEY";

        // 安 OAuth token / API key
        public const string GoogleApiKey = "AIzaSyEXAMPLE_FAKE_GOOGLE_API_KEY_12345";
        public const string StripeSecretKey = "sk_test_4eC39HqLyjWDarjtT1zdp7dc";

        // 安 JWT 芲
        public const string JwtSecret = "my_super_secret_jwt_signing_key_for_tests_only";

        // 安戈畐硈絬﹃ゅ瞷祘Α絏
        public const string ConnectionStringPlain = "Server=localhost;Database=TestDb;User Id=test;Password=P@ssw0rd!;";

        // 安╬ RSA 芲罽祏代刚ノ
        public const string PrivateRsaKeyPem = "-----BEGIN RSA PRIVATE KEY-----\nMIIEowIBAAKCAQEAEXAMPLEKEYDATA\n-----END RSA PRIVATE KEY-----";

        // 家览弄盞琵 scanner 盎代ノ猭
        public static void LogSecretsForTest()
        {
            // 珿種块度ㄑ代刚家览ぃ︽
            Console.WriteLine("AWS AccessKeyId: " + AwsAccessKeyId);
            Console.WriteLine("AWS SecretAccessKey: " + AwsSecretAccessKey);
            Console.WriteLine("Google API Key: " + GoogleApiKey);
            Console.WriteLine("Stripe Key: " + StripeSecretKey);
            Console.WriteLine("JWT Secret: " + JwtSecret);
            Console.WriteLine("DB ConnString: " + ConnectionStringPlain);
            Console.WriteLine("RSA Private Key (first 40 chars): " + (PrivateRsaKeyPem?.Substring(0, Math.Min(40, PrivateRsaKeyPem.Length))));
        }
    }
}