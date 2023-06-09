/**
 * <auto-generated>
 * Autogenerated by Thrift Compiler (0.18.1)
 * DO NOT EDIT UNLESS YOU ARE SURE THAT YOU KNOW WHAT YOU ARE DOING
 * </auto-generated>
 */
using System.Text;
using Thrift.Collections;
using Thrift.Protocol;
using Thrift.Protocol.Entities;
using Thrift.Protocol.Utilities;

#pragma warning disable IDE0079  // remove unnecessary pragmas
#pragma warning disable IDE0017  // object init can be simplified
#pragma warning disable IDE0028  // collection init can be simplified
#pragma warning disable IDE1006  // parts of the code use IDL spelling
#pragma warning disable CA1822   // empty DeepCopy() methods still non-static
#pragma warning disable IDE0083  // pattern matching "that is not SomeType" requires net5.0 but we still support earlier versions

namespace Google.Cloud.PubSub.Compression.Thrift
{

    public partial class Person : TBase
  {
    private global::Google.Cloud.PubSub.Compression.Thrift.Name _name;
    private List<global::Google.Cloud.PubSub.Compression.Thrift.Phone> _phones;

    public global::Google.Cloud.PubSub.Compression.Thrift.Name Name
    {
      get
      {
        return _name;
      }
      set
      {
        __isset.name = true;
        this._name = value;
      }
    }

    public List<global::Google.Cloud.PubSub.Compression.Thrift.Phone> Phones
    {
      get
      {
        return _phones;
      }
      set
      {
        __isset.phones = true;
        this._phones = value;
      }
    }


    public Isset __isset;
    public struct Isset
    {
      public bool name;
      public bool phones;
    }

    public Person()
    {
    }

    public Person DeepCopy()
    {
      var tmp10 = new Person();
      if((Name != null) && __isset.name)
      {
        tmp10.Name = (global::Google.Cloud.PubSub.Compression.Thrift.Name)this.Name.DeepCopy();
      }
      tmp10.__isset.name = this.__isset.name;
      if((Phones != null) && __isset.phones)
      {
        tmp10.Phones = this.Phones.DeepCopy();
      }
      tmp10.__isset.phones = this.__isset.phones;
      return tmp10;
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
              if (field.Type == TType.Struct)
              {
                Name = new global::Google.Cloud.PubSub.Compression.Thrift.Name();
                await Name.ReadAsync(iprot, cancellationToken);
              }
              else
              {
                await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
              }
              break;
            case 2:
              if (field.Type == TType.List)
              {
                {
                  var _list11 = await iprot.ReadListBeginAsync(cancellationToken);
                  Phones = new List<global::Google.Cloud.PubSub.Compression.Thrift.Phone>(_list11.Count);
                  for(int _i12 = 0; _i12 < _list11.Count; ++_i12)
                  {
                    global::Google.Cloud.PubSub.Compression.Thrift.Phone _elem13;
                    _elem13 = new global::Google.Cloud.PubSub.Compression.Thrift.Phone();
                    await _elem13.ReadAsync(iprot, cancellationToken);
                    Phones.Add(_elem13);
                  }
                  await iprot.ReadListEndAsync(cancellationToken);
                }
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
        var tmp14 = new TStruct("Person");
        await oprot.WriteStructBeginAsync(tmp14, cancellationToken);
        var tmp15 = new TField();
        if((Name != null) && __isset.name)
        {
          tmp15.Name = "name";
          tmp15.Type = TType.Struct;
          tmp15.ID = 1;
          await oprot.WriteFieldBeginAsync(tmp15, cancellationToken);
          await Name.WriteAsync(oprot, cancellationToken);
          await oprot.WriteFieldEndAsync(cancellationToken);
        }
        if((Phones != null) && __isset.phones)
        {
          tmp15.Name = "phones";
          tmp15.Type = TType.List;
          tmp15.ID = 2;
          await oprot.WriteFieldBeginAsync(tmp15, cancellationToken);
          await oprot.WriteListBeginAsync(new TList(TType.Struct, Phones.Count), cancellationToken);
          foreach (global::Google.Cloud.PubSub.Compression.Thrift.Phone _iter16 in Phones)
          {
            await _iter16.WriteAsync(oprot, cancellationToken);
          }
          await oprot.WriteListEndAsync(cancellationToken);
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
      if (!(that is Person other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return ((__isset.name == other.__isset.name) && ((!__isset.name) || (global::System.Object.Equals(Name, other.Name))))
        && ((__isset.phones == other.__isset.phones) && ((!__isset.phones) || (TCollections.Equals(Phones, other.Phones))));
    }

    public override int GetHashCode() {
      int hashcode = 157;
      unchecked {
        if((Name != null) && __isset.name)
        {
          hashcode = (hashcode * 397) + Name.GetHashCode();
        }
        if((Phones != null) && __isset.phones)
        {
          hashcode = (hashcode * 397) + TCollections.GetHashCode(Phones);
        }
      }
      return hashcode;
    }

    public override string ToString()
    {
      var tmp17 = new StringBuilder("Person(");
      int tmp18 = 0;
      if((Name != null) && __isset.name)
      {
        if(0 < tmp18++) { tmp17.Append(", "); }
        tmp17.Append("Name: ");
        Name.ToString(tmp17);
      }
      if((Phones != null) && __isset.phones)
      {
        if(0 < tmp18++) { tmp17.Append(", "); }
        tmp17.Append("Phones: ");
        Phones.ToString(tmp17);
      }
      tmp17.Append(')');
      return tmp17.ToString();
    }
  }

}
