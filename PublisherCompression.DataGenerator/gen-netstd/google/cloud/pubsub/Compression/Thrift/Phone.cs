/**
 * <auto-generated>
 * Autogenerated by Thrift Compiler (0.18.1)
 * DO NOT EDIT UNLESS YOU ARE SURE THAT YOU KNOW WHAT YOU ARE DOING
 * </auto-generated>
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Thrift;
using Thrift.Collections;
using Thrift.Protocol;
using Thrift.Protocol.Entities;
using Thrift.Protocol.Utilities;
using Thrift.Transport;
using Thrift.Transport.Client;
using Thrift.Transport.Server;
using Thrift.Processor;


#pragma warning disable IDE0079  // remove unnecessary pragmas
#pragma warning disable IDE0017  // object init can be simplified
#pragma warning disable IDE0028  // collection init can be simplified
#pragma warning disable IDE1006  // parts of the code use IDL spelling
#pragma warning disable CA1822   // empty DeepCopy() methods still non-static
#pragma warning disable IDE0083  // pattern matching "that is not SomeType" requires net5.0 but we still support earlier versions

namespace Google.Cloud.PubSub.Compression.Thrift
{

  public partial class Phone : TBase
  {
    private global::Google.Cloud.PubSub.Compression.Thrift.PhoneType _type;
    private int _number;

    /// <summary>
    /// 
    /// <seealso cref="global::Google.Cloud.PubSub.Compression.Thrift.PhoneType"/>
    /// </summary>
    public global::Google.Cloud.PubSub.Compression.Thrift.PhoneType Type
    {
      get
      {
        return _type;
      }
      set
      {
        __isset.type = true;
        this._type = value;
      }
    }

    public int Number
    {
      get
      {
        return _number;
      }
      set
      {
        __isset.number = true;
        this._number = value;
      }
    }


    public Isset __isset;
    public struct Isset
    {
      public bool type;
      public bool number;
    }

    public Phone()
    {
      this._type = global::Google.Cloud.PubSub.Compression.Thrift.PhoneType.MOBILE;
      this.__isset.type = true;
    }

    public Phone DeepCopy()
    {
      var tmp5 = new Phone();
      if(__isset.type)
      {
        tmp5.Type = this.Type;
      }
      tmp5.__isset.type = this.__isset.type;
      if(__isset.number)
      {
        tmp5.Number = this.Number;
      }
      tmp5.__isset.number = this.__isset.number;
      return tmp5;
    }

    public async global::System.Threading.Tasks.Task ReadAsync(TProtocol iprot, CancellationToken cancellationToken)
    {
      iprot.IncrementRecursionDepth();
      try
      {
        TField field;
        await iprot.ReadStructBeginAsync(cancellationToken);
        while (true)
        {
          field = await iprot.ReadFieldBeginAsync(cancellationToken);
          if (field.Type == TType.Stop)
          {
            break;
          }

          switch (field.ID)
          {
            case 1:
              if (field.Type == TType.I32)
              {
                Type = (global::Google.Cloud.PubSub.Compression.Thrift.PhoneType)await iprot.ReadI32Async(cancellationToken);
              }
              else
              {
                await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
              }
              break;
            case 2:
              if (field.Type == TType.I32)
              {
                Number = await iprot.ReadI32Async(cancellationToken);
              }
              else
              {
                await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
              }
              break;
            default: 
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
              break;
          }

          await iprot.ReadFieldEndAsync(cancellationToken);
        }

        await iprot.ReadStructEndAsync(cancellationToken);
      }
      finally
      {
        iprot.DecrementRecursionDepth();
      }
    }

    public async global::System.Threading.Tasks.Task WriteAsync(TProtocol oprot, CancellationToken cancellationToken)
    {
      oprot.IncrementRecursionDepth();
      try
      {
        var tmp6 = new TStruct("Phone");
        await oprot.WriteStructBeginAsync(tmp6, cancellationToken);
        var tmp7 = new TField();
        if(__isset.type)
        {
          tmp7.Name = "type";
          tmp7.Type = TType.I32;
          tmp7.ID = 1;
          await oprot.WriteFieldBeginAsync(tmp7, cancellationToken);
          await oprot.WriteI32Async((int)Type, cancellationToken);
          await oprot.WriteFieldEndAsync(cancellationToken);
        }
        if(__isset.number)
        {
          tmp7.Name = "number";
          tmp7.Type = TType.I32;
          tmp7.ID = 2;
          await oprot.WriteFieldBeginAsync(tmp7, cancellationToken);
          await oprot.WriteI32Async(Number, cancellationToken);
          await oprot.WriteFieldEndAsync(cancellationToken);
        }
        await oprot.WriteFieldStopAsync(cancellationToken);
        await oprot.WriteStructEndAsync(cancellationToken);
      }
      finally
      {
        oprot.DecrementRecursionDepth();
      }
    }

    public override bool Equals(object that)
    {
      if (!(that is Phone other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return ((__isset.type == other.__isset.type) && ((!__isset.type) || (global::System.Object.Equals(Type, other.Type))))
        && ((__isset.number == other.__isset.number) && ((!__isset.number) || (global::System.Object.Equals(Number, other.Number))));
    }

    public override int GetHashCode() {
      int hashcode = 157;
      unchecked {
        if(__isset.type)
        {
          hashcode = (hashcode * 397) + Type.GetHashCode();
        }
        if(__isset.number)
        {
          hashcode = (hashcode * 397) + Number.GetHashCode();
        }
      }
      return hashcode;
    }

    public override string ToString()
    {
      var tmp8 = new StringBuilder("Phone(");
      int tmp9 = 0;
      if(__isset.type)
      {
        if(0 < tmp9++) { tmp8.Append(", "); }
        tmp8.Append("Type: ");
        Type.ToString(tmp8);
      }
      if(__isset.number)
      {
        if(0 < tmp9++) { tmp8.Append(", "); }
        tmp8.Append("Number: ");
        Number.ToString(tmp8);
      }
      tmp8.Append(')');
      return tmp8.ToString();
    }
  }

}
