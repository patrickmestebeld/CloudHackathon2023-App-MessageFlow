﻿using MessageFlow.Core.Messaging.Entities;
using MessageFlow.Core.Messaging.Interfaces;
using MessageFlow.Infra.Messaging.Configuration;
using MessageFlow.Infra.Messaging.Mapping;
using MessageOracle;
using Microsoft.Extensions.Options;

namespace MessageFlow.Infra.Messaging.Services
{
    public class PersonalDataFetcher : IPersonalDataFetcher
    {
        private readonly PersonalDataFetcherOptions _options;

        public PersonalDataFetcher(IOptions<PersonalDataFetcherOptions> options)
        {
            _options = options.Value;
        }

        public Task<PersonalData> FetchPersonalDataAsync(Guid burgerKey)
            => new MessageOracleClient(_options.ClientBaseUrl, _options.ClientSubscriptionKey, new HttpClient()).PersonalAsync(burgerKey)
                .ContinueWith(t => t.Result.ToPersonalData());
    }
}
