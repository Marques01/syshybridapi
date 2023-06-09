﻿using BLL.Models;

namespace BLL.Repository.Interfaces
{
	public interface IPurchaseRepository
	{
		Task CreateAsync(Purchase purchase);

		Task UpdateAsync(Purchase purchase);

        Task DeleteAsync(int id);

        Task FinalizePurchaseAsync(Purchase purchase);

		Task<Purchase> GetPurchaseByIdAsync(int purchaseId);
		
	}
}
