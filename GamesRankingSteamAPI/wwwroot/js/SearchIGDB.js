function redirect() {
    var htmlString = "https://www.igdb.com/search?type=1&q=";
    var searchString = document.getElementById("searchInput").value;
    var validSearchString = searchString.split(' ').join('+');

    if (searchString === "") {
        alert("Search bar is empty!");
    }
    else {
        htmlString += validSearchString;
        window.location.href = htmlString;
    }
}

