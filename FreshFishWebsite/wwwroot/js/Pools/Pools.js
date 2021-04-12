function feedFish(poolId, storageId) {
    let url = `/Pool/FeedFish?poolId=${poolId}&storageId=${storageId}`;
    openModal(url, "#feedFishDiv", "#feedFishModalDiv")
}

function watchStatistics(poolId) {
    let url = `/Pool/WatchStatistics?poolId=${poolId}`;
    openModal(url, "#poolChartDiv", "#poolChartModalDiv")
}

function openModal(url, innerDiv, mainDiv) {
    $(innerDiv).load(url, function () {
        $(mainDiv).modal("show");
    });
}
