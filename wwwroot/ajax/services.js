function loadMoreServices(servicesCount, callback) {
    $.ajax({
        url: "/Services/LoadMoreServices",
        type: "GET",
        data: { currentServicesCount: servicesCount },
        success: callback, // Pass the callback function directly
        dataType: "html" // Specify the expected data type as HTML
    });
}
