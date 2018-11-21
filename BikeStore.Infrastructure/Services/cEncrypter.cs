using System;
using System.Security.Cryptography;
namespace BikeStore.Infrastructure.Services {
  class cEncrypter : IEncrypter {

    private static readonly int mDeriveBytesIterationsCount = 10000;
    private static readonly int mSaltSize = 40;



    public string GetSalt(string xValue) {

      if (string.IsNullOrWhiteSpace(xValue)) {

        throw new ArgumentException("sól niemoże być pusta", nameof(xValue));
      }

      var pRandom = new Random();
      var pSaltBytes = new byte[mSaltSize];
      var pRng = RandomNumberGenerator.Create();
      pRng.GetBytes(pSaltBytes);

      return Convert.ToBase64String(pSaltBytes);

    }

    public string GetHash(string xValue, string xSalt) {

      if (string.IsNullOrWhiteSpace(xValue)){
        throw new ArgumentException("Niemożna wygenerować hash z pustej wartość ");
      }

      if (string.IsNullOrWhiteSpace(xSalt)) {
        throw new ArgumentException("sól niemoże być pusta", nameof(xValue));
      }

      var pbkdf2 = new Rfc2898DeriveBytes(xValue, GetBytes(xSalt), mDeriveBytesIterationsCount);

      return Convert.ToBase64String(pbkdf2.GetBytes(mSaltSize));

    }

    private static byte[] GetBytes(string xValue) {

      var pBytes = new byte[xValue.Length * sizeof(char)];
      Buffer.BlockCopy(xValue.ToCharArray(), 0, pBytes, 0, pBytes.Length);

      return pBytes;

    }


  }
}
