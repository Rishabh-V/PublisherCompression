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

  public partial class Name : TBase
  {
    private string _firstName;
    private string _lastName;

    public string FirstName
    {
      get
      {
        return _firstName;
      }
      set
      {
        __isset.firstName = true;
        this._firstName = value;
      }
    }

    public string LastName
    {
      get
      {
        return _lastName;
      }
      set
      {
        __isset.lastName = true;
        this._lastName = value;
      }
    }


    public Isset __isset;
    public struct Isset
    {
      public bool firstName;
      public bool lastName;
    }

    public Name()
    {
    }

    public Name DeepCopy()
    {
      var tmp0 = new Name();
      if((FirstName != null) && __isset.firstName)
      {
        tmp0.FirstName = this.FirstName;
      }
      tmp0.__isset.firstName = this.__isset.firstName;
      if((LastName != null) && __isset.lastName)
      {
        tmp0.LastName = this.LastName;
      }
      tmp0.__isset.lastName = this.__isset.lastName;
      return tmp0;
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
              if (field.Type == TType.String)
              {
                FirstName = await iprot.ReadStringAsync(cancellationToken);
              }
              else
              {
                await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
              }
              break;
            case 2:
              if (field.Type == TType.String)
              {
                LastName = await iprot.ReadStringAsync(cancellationToken);
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
        var tmp1 = new TStruct("Name");
        await oprot.WriteStructBeginAsync(tmp1, cancellationToken);
        var tmp2 = new TField();
        if((FirstName != null) && __isset.firstName)
        {
          tmp2.Name = "firstName";
          tmp2.Type = TType.String;
          tmp2.ID = 1;
          await oprot.WriteFieldBeginAsync(tmp2, cancellationToken);
          await oprot.WriteStringAsync(FirstName, cancellationToken);
          await oprot.WriteFieldEndAsync(cancellationToken);
        }
        if((LastName != null) && __isset.lastName)
        {
          tmp2.Name = "lastName";
          tmp2.Type = TType.String;
          tmp2.ID = 2;
          await oprot.WriteFieldBeginAsync(tmp2, cancellationToken);
          await oprot.WriteStringAsync(LastName, cancellationToken);
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
      if (!(that is Name other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return ((__isset.firstName == other.__isset.firstName) && ((!__isset.firstName) || (global::System.Object.Equals(FirstName, other.FirstName))))
        && ((__isset.lastName == other.__isset.lastName) && ((!__isset.lastName) || (global::System.Object.Equals(LastName, other.LastName))));
    }

    public override int GetHashCode() {
      int hashcode = 157;
      unchecked {
        if((FirstName != null) && __isset.firstName)
        {
          hashcode = (hashcode * 397) + FirstName.GetHashCode();
        }
        if((LastName != null) && __isset.lastName)
        {
          hashcode = (hashcode * 397) + LastName.GetHashCode();
        }
      }
      return hashcode;
    }

    public override string ToString()
    {
      var tmp3 = new StringBuilder("Name(");
      int tmp4 = 0;
      if((FirstName != null) && __isset.firstName)
      {
        if(0 < tmp4++) { tmp3.Append(", "); }
        tmp3.Append("FirstName: ");
        FirstName.ToString(tmp3);
      }
      if((LastName != null) && __isset.lastName)
      {
        if(0 < tmp4++) { tmp3.Append(", "); }
        tmp3.Append("LastName: ");
        LastName.ToString(tmp3);
      }
      tmp3.Append(')');
      return tmp3.ToString();
    }
  }

}
