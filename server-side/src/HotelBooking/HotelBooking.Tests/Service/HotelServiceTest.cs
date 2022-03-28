using System.Collections.Generic;
using System.Threading.Tasks;
using HotelBooking.Contracts.Domain.Repositories;
using HotelBooking.Contracts.Dto.Data;
using HotelBooking.Contracts.Dto.Models.Search;
using HotelBooking.Services;
using Moq;
using NUnit.Framework;

namespace HotelBooking.Tests.Service
{
    [TestFixture]
    internal class HotelServiceTest : UnitTestBase
    {
        private const string GetAsyncMethodName = nameof(HotelService.GetAsync) + ". ";
        private const string GetSearchResultByPageMethodName = nameof(HotelService.GetSearchResultByPageAsync) + ". ";
        private const int DefaultPageSize = 20;
        private const int Page = 1;

        private Mock<IHotelRepository> hotelRepositoryMock;

        private HotelService hotelService;

        [SetUp]
        public void TestInitialize()
        {
            hotelRepositoryMock = MockRepository.Create<IHotelRepository>();
            hotelService = new HotelService(hotelRepositoryMock.Object);
        }

        [TestCase(TestName = GetAsyncMethodName + "Should return result got from hotelRepository GetAsync method")]
        public async Task GetAsyncTest()
        {
            IReadOnlyCollection<HotelData> expected = CreateHotelsData();
            SetupHotelRepositoryGetAsyncMock(expected);

            IReadOnlyCollection<HotelData> actual = await hotelService.GetAsync();

            Assert.AreEqual(expected, actual);
        }

        [TestCase(TestName =
            "CountSearchResultsAsync. should return result got from hotelRepository CountSearchResultsAsync method")]
        public async Task CountSearchResultsTest()
        {
            DataForSearch dataForSearch = CreateDataForSearch();
            int expected = 5;

            SetupHotelRepositoryCountSearchResultsMock(dataForSearch, expected);

            int actual = await hotelService.CountSearchResultsAsync(dataForSearch);

            Assert.AreEqual(expected, actual);
        }

        [TestCase(TestName = GetSearchResultByPageMethodName +
                             "Should return result got from hotelRepository GetSearchResultByPageAsync method when dataForSearch is not null")]
        public async Task GetSearchResultByPageTest()
        {
            DataForSearch dataForSearch = CreateDataForSearch();
            IReadOnlyCollection<HotelData> expected = CreateHotelsData();

            SetupHotelRepositoryGetSearchResultByPageMock(dataForSearch, expected);

            IReadOnlyCollection<HotelData> actual = await hotelService.GetSearchResultByPageAsync(dataForSearch);

            Assert.AreEqual(expected, actual);
        }

        [TestCase(TestName = GetSearchResultByPageMethodName +
            "Should return result got from hotelRepository GetByPageAsync method when dataForSearch is null")]
        public async Task GetSearchResultByPageNullDataForSearchTest()
        {
            IReadOnlyCollection<HotelData> expected = CreateHotelsData();

            SetupHotelRepositoryGetByPageMock(Page, expected);

            IReadOnlyCollection<HotelData> actual = await hotelService.GetSearchResultByPageAsync(null);

            Assert.AreEqual(expected, actual);
        }

        [TestCase(TestName = GetAsyncMethodName + " With id argument should return result got from hotelRepository GetAsync(id) method")]
        public async Task GetAsyncByIdTest()
        {
            int hotelId = 1;
            HotelData expected = CreateHotelData();

            SetupHotelRepositoryGetAsyncMock(hotelId, expected);

            HotelData actual = await hotelService.GetAsync(hotelId);

            Assert.AreEqual(expected, actual);
        }

        private void SetupHotelRepositoryGetAsyncMock(IReadOnlyCollection<HotelData> hotelsData)
            => hotelRepositoryMock
                .Setup(repository => repository.GetAsync())
                .ReturnsAsync(hotelsData);

        private void SetupHotelRepositoryCountSearchResultsMock(DataForSearch dataForSearch, int expectedCount)
            => hotelRepositoryMock
                .Setup(repository => repository.CountSearchResultsAsync(dataForSearch))
                .ReturnsAsync(expectedCount);

        private void SetupHotelRepositoryGetSearchResultByPageMock(
            DataForSearch dataForSearch,
            IReadOnlyCollection<HotelData> hotelsData)
                => hotelRepositoryMock
                    .Setup(repository => repository.GetSearchResultByPageAsync(dataForSearch, DefaultPageSize))
                    .ReturnsAsync(hotelsData);

        private void SetupHotelRepositoryGetByPageMock(
            int page,
            IReadOnlyCollection<HotelData> hotelsData)
            => hotelRepositoryMock
                .Setup(repository => repository.GetByPageAsync(page, DefaultPageSize))
                .ReturnsAsync(hotelsData);

        private void SetupHotelRepositoryGetAsyncMock(int id, HotelData hotelData)
            => hotelRepositoryMock
                .Setup(repository => repository.GetAsync(id))
                .ReturnsAsync(hotelData);

        private IReadOnlyCollection<HotelData> CreateHotelsData() => new List<HotelData>();

        private DataForSearch CreateDataForSearch() => new DataForSearch();

        private HotelData CreateHotelData() => new HotelData();
    }
}