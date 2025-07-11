﻿using System;
using System.Collections.Generic;

namespace Code.Infrastructure.Services.IAP
{
  public interface IIAPService : IService
  {
    bool IsInitialized { get; }
    event Action Initialized;
    void Initialize();
    List<ProductDescription> Products();
    void StartPurchase(string productId);
  }
}