﻿using Recodme.RD.FullStoQReborn.DataLayer.Queue;
using System;
using System.ComponentModel.DataAnnotations;
using WebAPI.Models.Base;

namespace WebAPI.Models.QueueViewModel
{
    public class ReservedQueueViewModel:BaseViewModel
    {
        [Display(Name = "Profile")]
        [Required(ErrorMessage = "Required Field")]
        public Guid ProfileId { get; set; }
        
        [Display(Name = "Establishment")]
        [Required(ErrorMessage = "Required Field")] 
        public Guid EstablishmentId { get; set; }

        [Display(Name = "Expiration Date")]
        public string ExpirationDate { get; set; }

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
                ExpirationDate = reservedQueue.CreatedAt.AddHours(2).ToString("dd-MM-yyyy HH:mm:ss"),
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
