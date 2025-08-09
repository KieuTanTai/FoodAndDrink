// File: DisposeReason.cs
using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public class DisposeReasonModel : IGetIdEntity<uint>
    {
        // Corresponds to 'dispose_reason_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint DisposeReasonId { get; private set; }

        // Corresponds to 'dispose_reason_name' (VARCHAR(30))
        public string DisposeReasonName { get; private set; }

        // Navigation property
        public ICollection<DisposeProductModel> DisposeProducts { get; private set; } = new List<DisposeProductModel>();

        public DisposeReasonModel(uint disposeReasonId, string disposeReasonName)
        {
            DisposeReasonId = disposeReasonId;
            DisposeReasonName = disposeReasonName;
        }

        public uint GetIdEntity() => DisposeReasonId;
    }
}

