using Recodme.RD.FullStoQReborn.DataLayer.Queue;
using System;
using WebAPI.Models.Base;

namespace WebAPI.Models.QueueViewModel
{
    public class StoreQueueViewModel : BaseViewModel
    {
        public int Quantity { get; set; }
        public Guid EstablishmentId { get; set; }

        public StoreQueue ToStoreQueue()
        {
            return new StoreQueue(Quantity, EstablishmentId);
        }
        public static StoreQueueViewModel Parse(StoreQueue storeQueue)
        {
            return new StoreQueueViewModel()
            {
                Quantity = storeQueue.Quantity,
                EstablishmentId = storeQueue.EstablishmentId
            };
        }

        public StoreQueue ToModel(StoreQueue model)
        {
            model.EstablishmentId = EstablishmentId;
            model.Quantity = Quantity;
            return model;
        }

        public bool CompareToModel(StoreQueue model)
        {
            return EstablishmentId == model.EstablishmentId && Quantity == model.Quantity;
        }
    }
}

