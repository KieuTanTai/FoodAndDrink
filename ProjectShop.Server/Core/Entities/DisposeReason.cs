// File: DisposeReason.cs
using ProjectShop.Server.Core.Interfaces.IEntities;
using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities
{
    public class DisposeReason : IGetIdEntity<uint>
    {
        // Corresponds to 'dispose_reason_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint DisposeReasonId { get; private set; }

        // Corresponds to 'dispose_reason_name' (VARCHAR(30))
        public string DisposeReasonName { get; private set; }

        // Navigation property
        public ICollection<DisposeProduct> DisposeProducts { get; private set; } = new List<DisposeProduct>();

        public DisposeReason(uint disposeReasonId, string disposeReasonName)
        {
            DisposeReasonId = disposeReasonId;
            DisposeReasonName = disposeReasonName;
        }

        public uint GetIdEntity() => DisposeReasonId;
    }
}

