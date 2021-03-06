﻿using Recodme.RD.FullStoQReborn.DataLayer.Queue;
using System;
using System.ComponentModel.DataAnnotations;
using WebAPI.Models.Base;

namespace WebAPI.Models.QueueViewModel
{
    public class StoreQueueViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "Required Field")] 
        public int Quantity { get; set; }
        
        [Display(Name = "Establishment")]
        [Required(ErrorMessage = "Required Field")] 
        public Guid EstablishmentId { get; set; }

        public StoreQueue ToStoreQueue()
        {
            return new StoreQueue(Quantity, EstablishmentId);
        }
        public static StoreQueueViewModel Parse(StoreQueue storeQueue)
        {
            return new StoreQueueViewModel()
            {
                Id = storeQueue.Id,
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

