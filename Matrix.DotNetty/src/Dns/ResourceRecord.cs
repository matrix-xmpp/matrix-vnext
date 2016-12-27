//
// Bdev.Net.Dns by Rob Philpott, Big Developments Ltd. Please send all bugs/enhancements to
// rob@bigdevelopments.co.uk  This file and the code contained within is freeware and may be
// distributed and edited without restriction.
// 

namespace Matrix.DotNetty.Dns
{
	/// <summary>
	/// Represents a Resource Record as detailed in RFC1035 4.1.3
	/// </summary>	
    internal class ResourceRecord
	{
		// private, constructor initialised fields
		private readonly string		_domain;
		private readonly DnsType	_dnsType;
		private readonly DnsClass	_dnsClass;
		private readonly int		_ttl;
		private readonly RecordBase	_record;

		// read only properties applicable for all records
		public string Domain
        { 
            get { return _domain; }
        }
		
        public DnsType Type
        {
            get { return _dnsType; }
        }

		public DnsClass	Class		   
        { 
            get { return _dnsClass;	}
        }
		
        public int Ttl
        { 
            get { return _ttl; }
        }

		public RecordBase Record
        { 
            get { return _record; }
        }

		/// <summary>
		/// Construct a resource record from a pointer to a byte array
		/// </summary>
		/// <param name="pointer">the position in the byte array of the record</param>
		internal ResourceRecord(Pointer pointer)
		{
			// extract the domain, question type, question class and Ttl
			_domain     = pointer.ReadDomain();
			_dnsType    = (DnsType) pointer.ReadShort();
			_dnsClass   = (DnsClass) pointer.ReadShort();
			_ttl        = pointer.ReadInt();

			// the next short is the record length, we only use it for unrecognised record types
			int recordLength = pointer.ReadShort();

			// and create the appropriate RDATA record based on the dnsType
			switch (_dnsType)
			{
                case DnsType.SRV:
                    _record = new SRVRecord(pointer);
                    break;
				
                default:
				{
					// move the pointer over this unrecognised record
					pointer.Position += recordLength;
					break;
				}
			}
		}
	}

	// Answers, Name Servers and Additional Records all share the same RR format	
	internal class Answer : ResourceRecord
	{
		internal Answer(Pointer pointer) : base(pointer) {}
	}
    	
	internal class NameServer : ResourceRecord
	{
		internal NameServer(Pointer pointer) : base(pointer) {}
	}
    	
	internal class AdditionalRecord : ResourceRecord
	{
		internal AdditionalRecord(Pointer pointer) : base(pointer) {}
	}
}