using System.Collections;
using System.Text;
using System.Net;
using System.IO;
using System;
using UnityEngine;

public static class MemoryStreamExtensions {
	public static MemoryStream Write(this MemoryStream ms, MemoryStream data) {
		ms.Write(data.GetBuffer(), 0, (int) data.Length);

		return ms;
	}

	public static MemoryStream Write(this MemoryStream ms, byte[] data) {
		ms.Write(data, 0, data.Length);

		return ms;
	}

	public static MemoryStream Read(this MemoryStream ms, byte[] data) {
		ms.Read(data, 0, data.Length);

		return ms;
	}

	public static int ReadVarInt(this MemoryStream ms) {
		var value = 0;
		var size = 0;
		int b;
		while (((b = ms.ReadByte()) & 0x80) == 0x80) {
			value |= (b & 0x7F) << (size++ * 7);
			if (size > 5) {
				throw new SystemException("VarInt is too big");
			}
		}
		return value | ((b & 0x7F) << (size * 7));
	}

	public static long ReadVarLong(this MemoryStream ms) {
		var value = 0;
		var size = 0;
		int b;
		while (((b = ms.ReadByte()) & 0x80) == 0x80) {
			value |= (b & 0x7F) << (size++ * 7);
			if (size > 10) {
				throw new SystemException("VarLong is too big");
			}
		}
		return value | ((b & 0x7F) << (size * 7));
	}

	public static string ReadString(this MemoryStream ms) {
		var length = ms.ReadVarInt();
		byte[] stringValue = new byte[length];
		ms.Read(stringValue, 0, length);

		return Encoding.UTF8.GetString(stringValue);
	}

	public static int ReadInt(this MemoryStream ms) {
		byte[] data = new byte[4];
		ms.Read(data, 0, data.Length);

		return data[0] << 24 |
			(data[1] & 0xFF) << 16 |
			(data[2] & 0xFF) << 8 |
			(data[3] & 0xFF);
	}

	public static unsafe float ReadFloat(this MemoryStream ms) {
		int i = ReadInt(ms);
		return *((float*) & i);
	}

	public static long ReadLong(this MemoryStream ms) {
		byte[] data = new byte[8];
		ms.Read(data, 0, data.Length);

		return ((((long) data[0]) << 56) |
			(((long) data[1] & 0xff) << 48) |
			(((long) data[2] & 0xff) << 40) |
			(((long) data[3] & 0xff) << 32) |
			(((long) data[4] & 0xff) << 24) |
			(((long) data[5] & 0xff) << 16) |
			(((long) data[6] & 0xff) << 8) |
			(((long) data[7] & 0xff)));
	}

	public static Uuid ReadUuid(this MemoryStream ms) {
        return new Uuid(ms.ReadLong(), ms.ReadLong());
    }

	public static bool ReadBool(this MemoryStream ms) {
		return ms.ReadByte() == 1;
	}

	public static Vector3Int ReadPosition(this MemoryStream ms) {
		long val = ms.ReadLong();

		int x = (int) (val << 64 - 38 - 26 >> 64 - 26);
        int y = (int) (val << 64 - 26 - 12 >> 64 - 12);
        int z = (int) (val << 64 - 26 >> 64 - 26);
		return new Vector3Int(x, y, z);
	}

	public static unsafe double ReadDouble(this MemoryStream ms) {
		long l = ReadLong(ms);
		return *((double*) & l);
	}

	public static sbyte ReadSByte(this MemoryStream ms) {
		return unchecked((sbyte) ms.ReadByte());
	}


	public static short ReadShort(this MemoryStream ms) {
		byte[] data = new byte[2];
		ms.Read(data, 0, data.Length);

		return (short) (data[0] << 8 | data[1]);
	}

	public static MemoryStream WriteUByte(this MemoryStream ms, byte value) {
		ms.WriteByte(value);

		return ms;
	}

	public static MemoryStream WriteSByte(this MemoryStream ms, sbyte value) {
		ms.WriteByte(unchecked((byte) value));

		return ms;
	}

	public static MemoryStream WriteVarInt(this MemoryStream ms, int value) {
		while ((value & -128) != 0) {
			ms.WriteByte((byte) (value & 127 | 128));
			value = (int) (((uint) value) >> 7);
		}
		ms.WriteByte((byte) value);

		return ms;
	}

	public static MemoryStream WriteVarLong(this MemoryStream ms, long value) {
		while ((value & ~0x7F) != 0) {
			ms.WriteByte((byte) ((value & 0x7F) | 0x80));
			value >>= 7;
		}
		ms.WriteByte((byte) value);

		return ms;
	}

	public static MemoryStream WriteString(this MemoryStream ms, string data) {
		byte[] buffer = Encoding.UTF8.GetBytes(data);
		WriteVarInt(ms, buffer.Length);
		ms.Write(buffer);

		return ms;
	}

	public static MemoryStream WriteUShort(this MemoryStream ms, ushort data) {
		byte[] uShortData = BitConverter.GetBytes(data);
		Array.Reverse(uShortData);
		ms.Write(uShortData);

		return ms;
	}

	public static MemoryStream WriteLong(this MemoryStream ms, long data) {
		byte[] longData = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(data));
		ms.Write(longData);

		return ms;
	}

	public static MemoryStream WriteInt(this MemoryStream ms, int data) {
		byte[] intData = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(data));
		ms.Write(intData);

		return ms;
	}

	public static unsafe MemoryStream WriteFloat(this MemoryStream ms, float data) {
		return WriteInt(ms, *((int*) & data));
	}

	public static unsafe MemoryStream WriteDouble(this MemoryStream ms, double data) {
		return WriteLong(ms, *((long*) & data));
	}

	public static MemoryStream WriteBool(this MemoryStream ms, bool data) {
		ms.WriteByte((BitConverter.GetBytes(data)[0]));

		return ms;
	}
}
