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

  public partial class Company : TBase
  {
    private string _name;
    private global::Google.Cloud.PubSub.Compression.Thrift.Address _headQuarter;
    private List<global::Google.Cloud.PubSub.Compression.Thrift.Address> _offices;
    private string _establishDate;
    private string _description;
    private int _employeeCount;
    private List<global::Google.Cloud.PubSub.Compression.Thrift.Person> _founders;

    public string Name
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

    public global::Google.Cloud.PubSub.Compression.Thrift.Address HeadQuarter
    {
      get
      {
        return _headQuarter;
      }
      set
      {
        __isset.headQuarter = true;
        this._headQuarter = value;
      }
    }

    public List<global::Google.Cloud.PubSub.Compression.Thrift.Address> Offices
    {
      get
      {
        return _offices;
      }
      set
      {
        __isset.offices = true;
        this._offices = value;
      }
    }

    public string EstablishDate
    {
      get
      {
        return _establishDate;
      }
      set
      {
        __isset.establishDate = true;
        this._establishDate = value;
      }
    }

    public string Description
    {
      get
      {
        return _description;
      }
      set
      {
        __isset.description = true;
        this._description = value;
      }
    }

    public int EmployeeCount
    {
      get
      {
        return _employeeCount;
      }
      set
      {
        __isset.employeeCount = true;
        this._employeeCount = value;
      }
    }

    public List<global::Google.Cloud.PubSub.Compression.Thrift.Person> Founders
    {
      get
      {
        return _founders;
      }
      set
      {
        __isset.founders = true;
        this._founders = value;
      }
    }


    public Isset __isset;
    public struct Isset
    {
      public bool name;
      public bool headQuarter;
      public bool offices;
      public bool establishDate;
      public bool description;
      public bool employeeCount;
      public bool founders;
    }

    public Company()
    {
    }

    public Company DeepCopy()
    {
      var tmp10 = new Company();
      if((Name != null) && __isset.name)
      {
        tmp10.Name = this.Name;
      }
      tmp10.__isset.name = this.__isset.name;
      if((HeadQuarter != null) && __isset.headQuarter)
      {
        tmp10.HeadQuarter = (global::Google.Cloud.PubSub.Compression.Thrift.Address)this.HeadQuarter.DeepCopy();
      }
      tmp10.__isset.headQuarter = this.__isset.headQuarter;
      if((Offices != null) && __isset.offices)
      {
        tmp10.Offices = this.Offices.DeepCopy();
      }
      tmp10.__isset.offices = this.__isset.offices;
      if((EstablishDate != null) && __isset.establishDate)
      {
        tmp10.EstablishDate = this.EstablishDate;
      }
      tmp10.__isset.establishDate = this.__isset.establishDate;
      if((Description != null) && __isset.description)
      {
        tmp10.Description = this.Description;
      }
      tmp10.__isset.description = this.__isset.description;
      if(__isset.employeeCount)
      {
        tmp10.EmployeeCount = this.EmployeeCount;
      }
      tmp10.__isset.employeeCount = this.__isset.employeeCount;
      if((Founders != null) && __isset.founders)
      {
        tmp10.Founders = Founders.DeepCopy();
      }
      tmp10.__isset.founders = this.__isset.founders;
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
              if (field.Type == TType.String)
              {
                Name = await iprot.ReadStringAsync(cancellationToken);
              }
              else
              {
                await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
              }
              break;
            case 2:
              if (field.Type == TType.Struct)
              {
                HeadQuarter = new global::Google.Cloud.PubSub.Compression.Thrift.Address();
                await HeadQuarter.ReadAsync(iprot, cancellationToken);
              }
              else
              {
                await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
              }
              break;
            case 3:
              if (field.Type == TType.List)
              {
                {
                  var _list11 = await iprot.ReadListBeginAsync(cancellationToken);
                  Offices = new List<global::Google.Cloud.PubSub.Compression.Thrift.Address>(_list11.Count);
                  for(int _i12 = 0; _i12 < _list11.Count; ++_i12)
                  {
                    global::Google.Cloud.PubSub.Compression.Thrift.Address _elem13;
                    _elem13 = new global::Google.Cloud.PubSub.Compression.Thrift.Address();
                    await _elem13.ReadAsync(iprot, cancellationToken);
                    Offices.Add(_elem13);
                  }
                  await iprot.ReadListEndAsync(cancellationToken);
                }
              }
              else
              {
                await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
              }
              break;
            case 4:
              if (field.Type == TType.String)
              {
                EstablishDate = await iprot.ReadStringAsync(cancellationToken);
              }
              else
              {
                await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
              }
              break;
            case 5:
              if (field.Type == TType.String)
              {
                Description = await iprot.ReadStringAsync(cancellationToken);
              }
              else
              {
                await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
              }
              break;
            case 6:
              if (field.Type == TType.I32)
              {
                EmployeeCount = await iprot.ReadI32Async(cancellationToken);
              }
              else
              {
                await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
              }
              break;
            case 7:
              if (field.Type == TType.List)
              {
                {
                  var _list14 = await iprot.ReadListBeginAsync(cancellationToken);
                  Founders = new List<global::Google.Cloud.PubSub.Compression.Thrift.Person>(_list14.Count);
                  for(int _i15 = 0; _i15 < _list14.Count; ++_i15)
                  {
                    global::Google.Cloud.PubSub.Compression.Thrift.Person _elem16;
                    _elem16 = new global::Google.Cloud.PubSub.Compression.Thrift.Person();
                    await _elem16.ReadAsync(iprot, cancellationToken);
                    Founders.Add(_elem16);
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
        var tmp17 = new TStruct("Company");
        await oprot.WriteStructBeginAsync(tmp17, cancellationToken);
        var tmp18 = new TField();
        if((Name != null) && __isset.name)
        {
          tmp18.Name = "name";
          tmp18.Type = TType.String;
          tmp18.ID = 1;
          await oprot.WriteFieldBeginAsync(tmp18, cancellationToken);
          await oprot.WriteStringAsync(Name, cancellationToken);
          await oprot.WriteFieldEndAsync(cancellationToken);
        }
        if((HeadQuarter != null) && __isset.headQuarter)
        {
          tmp18.Name = "headQuarter";
          tmp18.Type = TType.Struct;
          tmp18.ID = 2;
          await oprot.WriteFieldBeginAsync(tmp18, cancellationToken);
          await HeadQuarter.WriteAsync(oprot, cancellationToken);
          await oprot.WriteFieldEndAsync(cancellationToken);
        }
        if((Offices != null) && __isset.offices)
        {
          tmp18.Name = "offices";
          tmp18.Type = TType.List;
          tmp18.ID = 3;
          await oprot.WriteFieldBeginAsync(tmp18, cancellationToken);
          await oprot.WriteListBeginAsync(new TList(TType.Struct, Offices.Count), cancellationToken);
          foreach (global::Google.Cloud.PubSub.Compression.Thrift.Address _iter19 in Offices)
          {
            await _iter19.WriteAsync(oprot, cancellationToken);
          }
          await oprot.WriteListEndAsync(cancellationToken);
          await oprot.WriteFieldEndAsync(cancellationToken);
        }
        if((EstablishDate != null) && __isset.establishDate)
        {
          tmp18.Name = "establishDate";
          tmp18.Type = TType.String;
          tmp18.ID = 4;
          await oprot.WriteFieldBeginAsync(tmp18, cancellationToken);
          await oprot.WriteStringAsync(EstablishDate, cancellationToken);
          await oprot.WriteFieldEndAsync(cancellationToken);
        }
        if((Description != null) && __isset.description)
        {
          tmp18.Name = "description";
          tmp18.Type = TType.String;
          tmp18.ID = 5;
          await oprot.WriteFieldBeginAsync(tmp18, cancellationToken);
          await oprot.WriteStringAsync(Description, cancellationToken);
          await oprot.WriteFieldEndAsync(cancellationToken);
        }
        if(__isset.employeeCount)
        {
          tmp18.Name = "employeeCount";
          tmp18.Type = TType.I32;
          tmp18.ID = 6;
          await oprot.WriteFieldBeginAsync(tmp18, cancellationToken);
          await oprot.WriteI32Async(EmployeeCount, cancellationToken);
          await oprot.WriteFieldEndAsync(cancellationToken);
        }
        if((Founders != null) && __isset.founders)
        {
          tmp18.Name = "founders";
          tmp18.Type = TType.List;
          tmp18.ID = 7;
          await oprot.WriteFieldBeginAsync(tmp18, cancellationToken);
          await oprot.WriteListBeginAsync(new TList(TType.Struct, Founders.Count), cancellationToken);
          foreach (global::Google.Cloud.PubSub.Compression.Thrift.Person _iter20 in Founders)
          {
            await _iter20.WriteAsync(oprot, cancellationToken);
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
      if (!(that is Company other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return ((__isset.name == other.__isset.name) && ((!__isset.name) || (global::System.Object.Equals(Name, other.Name))))
        && ((__isset.headQuarter == other.__isset.headQuarter) && ((!__isset.headQuarter) || (global::System.Object.Equals(HeadQuarter, other.HeadQuarter))))
        && ((__isset.offices == other.__isset.offices) && ((!__isset.offices) || (TCollections.Equals(Offices, other.Offices))))
        && ((__isset.establishDate == other.__isset.establishDate) && ((!__isset.establishDate) || (global::System.Object.Equals(EstablishDate, other.EstablishDate))))
        && ((__isset.description == other.__isset.description) && ((!__isset.description) || (global::System.Object.Equals(Description, other.Description))))
        && ((__isset.employeeCount == other.__isset.employeeCount) && ((!__isset.employeeCount) || (global::System.Object.Equals(EmployeeCount, other.EmployeeCount))))
        && ((__isset.founders == other.__isset.founders) && ((!__isset.founders) || (TCollections.Equals(Founders, other.Founders))));
    }

    public override int GetHashCode() {
      int hashcode = 157;
      unchecked {
        if((Name != null) && __isset.name)
        {
          hashcode = (hashcode * 397) + Name.GetHashCode();
        }
        if((HeadQuarter != null) && __isset.headQuarter)
        {
          hashcode = (hashcode * 397) + HeadQuarter.GetHashCode();
        }
        if((Offices != null) && __isset.offices)
        {
          hashcode = (hashcode * 397) + TCollections.GetHashCode(Offices);
        }
        if((EstablishDate != null) && __isset.establishDate)
        {
          hashcode = (hashcode * 397) + EstablishDate.GetHashCode();
        }
        if((Description != null) && __isset.description)
        {
          hashcode = (hashcode * 397) + Description.GetHashCode();
        }
        if(__isset.employeeCount)
        {
          hashcode = (hashcode * 397) + EmployeeCount.GetHashCode();
        }
        if((Founders != null) && __isset.founders)
        {
          hashcode = (hashcode * 397) + TCollections.GetHashCode(Founders);
        }
      }
      return hashcode;
    }

    public override string ToString()
    {
      var tmp21 = new StringBuilder("Company(");
      int tmp22 = 0;
      if((Name != null) && __isset.name)
      {
        if(0 < tmp22++) { tmp21.Append(", "); }
        tmp21.Append("Name: ");
        Name.ToString(tmp21);
      }
      if((HeadQuarter != null) && __isset.headQuarter)
      {
        if(0 < tmp22++) { tmp21.Append(", "); }
        tmp21.Append("HeadQuarter: ");
        HeadQuarter.ToString(tmp21);
      }
      if((Offices != null) && __isset.offices)
      {
        if(0 < tmp22++) { tmp21.Append(", "); }
        tmp21.Append("Offices: ");
        Offices.ToString(tmp21);
      }
      if((EstablishDate != null) && __isset.establishDate)
      {
        if(0 < tmp22++) { tmp21.Append(", "); }
        tmp21.Append("EstablishDate: ");
        EstablishDate.ToString(tmp21);
      }
      if((Description != null) && __isset.description)
      {
        if(0 < tmp22++) { tmp21.Append(", "); }
        tmp21.Append("Description: ");
        Description.ToString(tmp21);
      }
      if(__isset.employeeCount)
      {
        if(0 < tmp22++) { tmp21.Append(", "); }
        tmp21.Append("EmployeeCount: ");
        EmployeeCount.ToString(tmp21);
      }
      if((Founders != null) && __isset.founders)
      {
        if(0 < tmp22++) { tmp21.Append(", "); }
        tmp21.Append("Founders: ");
        Founders.ToString(tmp21);
      }
      tmp21.Append(')');
      return tmp21.ToString();
    }
  }

}