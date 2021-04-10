const feedBtn  = document.querySelectorAll("#feed");

function feedFish(poolId, storageId) {
    let url = `/Pool/FeedFish?poolId=${poolId}&storageId=${storageId}`;
    $("#poolChartDiv").load(url, function () {
        $("#poolChartModalDiv").modal("show");
    });
}