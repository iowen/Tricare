﻿using System;
using TriCare.iOS;
using TriCare.Data;
using Xamarin.Forms;
using System.IO;
using System.Security.Cryptography;
using System.Text;
[assembly: Dependency (typeof (Encrypter_iOS))]
namespace TriCare.iOS
{
	public class Encrypter_iOS : IEncrypter
	{
		private static readonly byte[] salt = Encoding.ASCII.GetBytes("Xamarin.iOS Version: 7.0.6.168");

		public Encrypter_iOS ()
		{
		}
		public string GetEncryption (string input)
		{
			try {
				if (string.IsNullOrWhiteSpace (input.Trim ()))
					return string.Empty;
				var key = new Rfc2898DeriveBytes("password", salt);

				var algorithm = new RijndaelManaged ();
				int bytesForKey = algorithm.KeySize / 8;
				int bytesForIV = algorithm.BlockSize / 8;
				algorithm.Key = key.GetBytes (bytesForKey);
				algorithm.IV = key.GetBytes (bytesForIV);

				byte[] encryptedBytes;
				using (ICryptoTransform encryptor = algorithm.CreateEncryptor (algorithm.Key, algorithm.IV)) {
					byte[] bytesToEncrypt = Encoding.UTF8.GetBytes (input.Trim ());
					MemoryStream memory = new MemoryStream ();
					using (Stream stream = new CryptoStream (memory, encryptor, CryptoStreamMode.Write)) {
						stream.Write (bytesToEncrypt, 0, bytesToEncrypt.Length);
					}
					encryptedBytes = memory.ToArray();
				}
				return Convert.ToBase64String (encryptedBytes);
			} catch (Exception exx) {
				return string.Empty;
			}
		}
		public string GetDecryption(string input)
		{
			try{
				if (string.IsNullOrWhiteSpace (input.Trim ()))
					return string.Empty;
				var key = new Rfc2898DeriveBytes("password", salt);
				var algorithm = new RijndaelManaged ();
				int bytesForKey = algorithm.KeySize / 8;
				int bytesForIV = algorithm.BlockSize / 8;
				algorithm.Key = key.GetBytes (bytesForKey);
				algorithm.IV = key.GetBytes (bytesForIV);

				//Anything to process?

				byte[] descryptedBytes;
				using (ICryptoTransform decryptor = algorithm.CreateDecryptor(algorithm.Key, algorithm.IV))
				{
					byte[] encryptedBytes = Convert.FromBase64String(input.Trim());
					MemoryStream memory = new MemoryStream ();
					using (Stream stream = new CryptoStream (memory, decryptor, CryptoStreamMode.Write)) {
						stream.Write (encryptedBytes, 0, encryptedBytes.Length);
					}
					descryptedBytes = memory.ToArray();
				}
				return Encoding.UTF8.GetString(descryptedBytes);
			}
			catch (Exception exxx) {
				return string.Empty;
			}
		}
	}
}

