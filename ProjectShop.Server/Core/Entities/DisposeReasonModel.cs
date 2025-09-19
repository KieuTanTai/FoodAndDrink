// File: DisposeReason.cs
using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public class DisposeReasonModel : IGetIdEntity<uint>
    {
        // Backing fields
        private uint _disposeReasonId;
        private string _disposeReasonName = string.Empty;

        // Corresponds to 'dispose_reason_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint DisposeReasonId
        {
            get => _disposeReasonId;
            set => _disposeReasonId = value;
        }

        // Corresponds to 'dispose_reason_name' (VARCHAR(30))
        public string DisposeReasonName
        {
            get => _disposeReasonName;
            set => _disposeReasonName = value ?? string.Empty;
        }

        // Navigation properties
        public ICollection<DisposeProductModel> DisposeProducts { get; set; } = new List<DisposeProductModel>();
        // End of navigation properties

        public DisposeReasonModel(uint disposeReasonId, string disposeReasonName)
        {
            DisposeReasonId = disposeReasonId;
            DisposeReasonName = disposeReasonName;
        }

        public DisposeReasonModel() { }

        public uint GetIdEntity() => DisposeReasonId;
    }
}

