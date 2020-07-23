using Recodme.RD.FullStoQReborn.DataLayer.Queue;
using System;
using WebAPI.Models.Base;

namespace WebAPI.Models.QueueViewModel
{
    public class ReservedQueueViewModel:BaseViewModel
    {
        public Guid ProfileId { get; set; }
        public Guid EstablishmentId { get; set; }

        public ReservedQueue ToReservedQueue()
        {
            return new ReservedQueue(EstablishmentId, ProfileId);
        }
        public static ReservedQueueViewModel Parse(ReservedQueue reservedQueue)
        {
            return new ReservedQueueViewModel()
            {
                Id = reservedQueue.Id,
                ProfileId = reservedQueue.ProfileId,
                EstablishmentId = reservedQueue.EstablishmentId,
                //Id = reservedQueue.Id
            };
        }

        public ReservedQueue ToModel(ReservedQueue model)
        {
            model.EstablishmentId = EstablishmentId;
            model.ProfileId = ProfileId;
            return model;
        }

        public bool CompareToModel(ReservedQueue model)
        {
            return EstablishmentId == model.EstablishmentId && ProfileId == model.ProfileId;
        }
    }
}
