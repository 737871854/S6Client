using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.IO;
using System.Text;

public class RSACryptoService
{
	private RSACryptoServiceProvider _privateKeyRsaProvider;
	private RSACryptoServiceProvider _publicKeyRsaProvider;

	private RSAParameters _privateKeyRsaParameter;
	private RSAParameters _publicKeyRsaParameter;

	public RSACryptoService(string privateKey = null, string publicKey = null)
	{
		if (!string.IsNullOrEmpty(privateKey))
		{
			_privateKeyRsaProvider = CreateRsaProviderFromPrivateKey(privateKey);
		}

		if (!string.IsNullOrEmpty(publicKey))
		{
			_publicKeyRsaProvider = CreateRsaProviderFromPublicKey(publicKey);
		}
	}

	private RSACryptoServiceProvider CreateRsaProviderFromPrivateKey(string privateKey)
	{
		var privateKeyBits = System.Convert.FromBase64String(privateKey);
		var RSA = new RSACryptoServiceProvider();
		_privateKeyRsaParameter = new RSAParameters();
		
		using (BinaryReader binr = new BinaryReader(new MemoryStream(privateKeyBits)))
		{
			byte bt = 0;
			ushort twobytes = 0;
			twobytes = binr.ReadUInt16();
			if (twobytes == 0x8130)
				binr.ReadByte();
			else if (twobytes == 0x8230)
				binr.ReadInt16();
			else
				throw new Exception("Unexpected value read binr.ReadUInt16()");
			
			twobytes = binr.ReadUInt16();
			if (twobytes != 0x0102)
				throw new Exception("Unexpected version");
			
			bt = binr.ReadByte();
			if (bt != 0x00)
				throw new Exception("Unexpected value read binr.ReadByte()");
			
			_privateKeyRsaParameter.Modulus = binr.ReadBytes(GetIntegerSize(binr));
			_privateKeyRsaParameter.Exponent = binr.ReadBytes(GetIntegerSize(binr));
			_privateKeyRsaParameter.D = binr.ReadBytes(GetIntegerSize(binr));
			_privateKeyRsaParameter.P = binr.ReadBytes(GetIntegerSize(binr));
			_privateKeyRsaParameter.Q = binr.ReadBytes(GetIntegerSize(binr));
			_privateKeyRsaParameter.DP = binr.ReadBytes(GetIntegerSize(binr));
			_privateKeyRsaParameter.DQ = binr.ReadBytes(GetIntegerSize(binr));
			_privateKeyRsaParameter.InverseQ = binr.ReadBytes(GetIntegerSize(binr));
		}
		
		RSA.ImportParameters(_privateKeyRsaParameter);
		return RSA;
	}

	private int GetIntegerSize(BinaryReader binr)
	{
		byte bt = 0;
		byte lowbyte = 0x00;
		byte highbyte = 0x00;
		int count = 0;
		bt = binr.ReadByte();
		if (bt != 0x02)
			return 0;
		bt = binr.ReadByte();
		
		if (bt == 0x81)
			count = binr.ReadByte();
		else
			if (bt == 0x82)
		{
			highbyte = binr.ReadByte();
			lowbyte = binr.ReadByte();
			byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };
			count = BitConverter.ToInt32(modint, 0);
		}
		else
		{
			count = bt;
		}
		
		while (binr.ReadByte() == 0x00)
		{
			count -= 1;
		}
		binr.BaseStream.Seek(-1, SeekOrigin.Current);
		return count;
	}

	private RSACryptoServiceProvider CreateRsaProviderFromPublicKey(string publicKeyString)
	{
		// encoded OID sequence for  PKCS #1 rsaEncryption szOID_RSA_RSA = "1.2.840.113549.1.1.1"
		byte[] SeqOID = { 0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00 };
		byte[] x509key;
		byte[] seq = new byte[15];
		int x509size;
		
		x509key = Convert.FromBase64String(publicKeyString);
		x509size = x509key.Length;
		
		// ---------  Set up stream to read the asn.1 encoded SubjectPublicKeyInfo blob  ------
		using (MemoryStream mem = new MemoryStream(x509key))
		{
			using (BinaryReader binr = new BinaryReader(mem))  //wrap Memory Stream with BinaryReader for easy reading
			{
				byte bt = 0;
				ushort twobytes = 0;
				
				twobytes = binr.ReadUInt16();
				if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)
					binr.ReadByte();    //advance 1 byte
				else if (twobytes == 0x8230)
					binr.ReadInt16();   //advance 2 bytes
				else
				{
					return null;
				}
				
				seq = binr.ReadBytes(15);       //read the Sequence OID
				if (!CompareBytearrays(seq, SeqOID))    //make sure Sequence for OID is correct
				{
					return null;
				}
				
				twobytes = binr.ReadUInt16();
				if (twobytes == 0x8103) //data read as little endian order (actual data order for Bit String is 03 81)
					binr.ReadByte();    //advance 1 byte
				else if (twobytes == 0x8203)
					binr.ReadInt16();   //advance 2 bytes
				else
				{
					return null;
				}
				
				bt = binr.ReadByte();
				if (bt != 0x00)     //expect null byte next
				{
					return null;
				}
				
				twobytes = binr.ReadUInt16();
				if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)
					binr.ReadByte();    //advance 1 byte
				else if (twobytes == 0x8230)
					binr.ReadInt16();   //advance 2 bytes
				else
				{
					return null;
				}
				
				twobytes = binr.ReadUInt16();
				byte lowbyte = 0x00;
				byte highbyte = 0x00;
				
				if (twobytes == 0x8102) //data read as little endian order (actual data order for Integer is 02 81)
					lowbyte = binr.ReadByte();  // read next bytes which is bytes in modulus
				else if (twobytes == 0x8202)
				{
					highbyte = binr.ReadByte(); //advance 2 bytes
					lowbyte = binr.ReadByte();
				}
				else
					return null;
				byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };   //reverse byte order since asn.1 key uses big endian order
				int modsize = BitConverter.ToInt32(modint, 0);
				
				int firstbyte = binr.PeekChar();
				if (firstbyte == 0x00)
				{   //if first byte (highest order) of modulus is zero, don't include it
					binr.ReadByte();    //skip this null byte
					modsize -= 1;   //reduce modulus buffer size by 1
				}
				
				byte[] modulus = binr.ReadBytes(modsize);   //read the modulus bytes
				
				if (binr.ReadByte() != 0x02)            //expect an Integer for the exponent data
				{
					return null;
				}
				int expbytes = (int)binr.ReadByte();        // should only need one byte for actual exponent data (for all useful values)
				byte[] exponent = binr.ReadBytes(expbytes);
				
				// ------- create RSACryptoServiceProvider instance and initialize with public key -----
				RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
				_publicKeyRsaParameter = new RSAParameters();
				_publicKeyRsaParameter.Modulus = modulus;
				_publicKeyRsaParameter.Exponent = exponent;
				RSA.ImportParameters(_publicKeyRsaParameter);
				
				return RSA;
			}	
		}
	}

	private bool CompareBytearrays(byte[] a, byte[] b)
	{
		if (a.Length != b.Length)
			return false;
		int i = 0;
		foreach (byte c in a)
		{
			if (c != b[i])
				return false;
			i++;
		}
		return true;
	}

	public byte[] Decrypt(byte[] cipherText)
	{
		if (_privateKeyRsaProvider == null)
		{
			throw new Exception("_privateKeyRsaProvider is null");
		}

		byte[] cipherbytes;
		cipherbytes = _privateKeyRsaProvider.Decrypt(cipherText, false);
		//cipherbytes = _publicKeyRsaProvider.Decrypt(cipherText, false);
		return cipherbytes;
	}

	public byte[] Encrypt(byte[] cipherText)
	{
		if (_publicKeyRsaProvider == null)
		{
			throw new Exception("_publicKeyRsaProvider is null");
		}
		byte[] cipherbytes;
		cipherbytes = _publicKeyRsaProvider.Encrypt (cipherText, false);
		//cipherbytes = _privateKeyRsaProvider.Encrypt (cipherText, false);
		return cipherbytes;
	}

	////////////////
	//下面的是私钥加密，公钥解密接口
	////////////////
	/*
	public byte[] Encrypt(byte[] source)
	{
		BigInteger d = new BigInteger(_privateKeyRsaParameter.D);
		BigInteger n = new BigInteger(_privateKeyRsaParameter.Modulus);
		int sug = 127;
		int len = source.Length;
		int cycle = 0;
		if ((len % sug) == 0) cycle = len / sug; else cycle = len / sug + 1;
		
		ArrayList temp = new ArrayList();
		int blockLen = 0;
		for (int i = 0; i < cycle; i++)
		{
			if (len >= sug) blockLen = sug; else blockLen = len;
			
			byte[] context = new byte[blockLen];
			int po = i * sug;
			Array.Copy(source, po, context, 0, blockLen);
			
			BigInteger biText = new BigInteger(context);
			BigInteger biEnText = biText.modPow(d, n);
			
			byte[] b = biEnText.getBytes();
			temp.AddRange(b);
			len -= blockLen;
		}
		return (byte[])temp.ToArray(typeof(byte));
	}

	public byte[] Decrypt(byte[] source)
	{
		BigInteger e = new BigInteger(_publicKeyRsaParameter.Exponent);
		BigInteger n = new BigInteger(_publicKeyRsaParameter.Modulus);
		
		int bk = 128;
		int len = source.Length;
		int cycle = 0;
		if ((len % bk) == 0) cycle = len / bk; else cycle = len / bk + 1;
		
		ArrayList temp = new ArrayList();
		int blockLen = 0;
		for (int i = 0; i < cycle; i++)
		{
			if (len >= bk) blockLen = bk; else blockLen = len;
			
			byte[] context = new byte[blockLen];
			int po = i * bk;
			Array.Copy(source, po, context, 0, blockLen);
			
			BigInteger biText = new BigInteger(context);
			BigInteger biEnText = biText.modPow(e, n);
			
			byte[] b = biEnText.getBytes();
			temp.AddRange(b);
			len -= blockLen;
		}
		return (byte[])temp.ToArray(typeof(byte));
	}
	*/
}