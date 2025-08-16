// File: DisposeReason.cs
using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public class DisposeReasonModel : IGetIdEntity<uint>
    {
        // Corresponds to 'dispose_reason_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint DisposeReasonId { get; set; }

        // Corresponds to 'dispose_reason_name' (VARCHAR(30))
        public string DisposeReasonName { get; set; } = string.Empty;

        // Navigation property
        public ICollection<DisposeProductModel> DisposeProducts { get; set; } = new List<DisposeProductModel>();

        public DisposeReasonModel(uint disposeReasonId, string disposeReasonName)
        {
            DisposeReasonId = disposeReasonId;
            DisposeReasonName = disposeReasonName;
        }

        public DisposeReasonModel() { }

        public uint GetIdEntity() => DisposeReasonId;
    }
}

