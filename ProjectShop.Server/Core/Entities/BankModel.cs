﻿using ProjectShop.Server.Core.Enums;

namespace ProjectShop.Server.Core.Entities
{
    public class BankModel
    {
        public int BankId { get; private set; }
        public string BankName { get; private set; } = string.Empty;
        public EActiveStatus Status { get; private set; }

        public BankModel() { }

        public BankModel(int id, string name, EActiveStatus status)
        {
            BankId = id;
            BankName = name;
            Status = status;
        }

        public void SetBankId(int id)
        {
            BankId = id;
        }

        public void SetBankName(string name)
        {
            BankName = name;
        }

        public void SetStatus(EActiveStatus status)
        {
            Status = status;
        }
    }
}
