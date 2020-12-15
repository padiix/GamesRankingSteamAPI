$(document).ready(function () {
    $.getJSON('/api/IntGam.json', function (jd) {
        $(".game-list").each(function (i, elem) {
            var releaseYear = new Date(jd.data[i].firstReleaseDate);
            releaseYear = releaseYear.getFullYear();
            var nr = i + 1;
            $(elem).html('<h3>' + nr + '. ' + jd.data[i].title + '</h3>');
            var genres = jd.data[i].top15interestinggamesHG;
            var genresArray = [];
            $.each(genres, function(j, genres) {
                genresArray.push('> '+ genres.genresGenre.name + '<br>');
            });
            $(elem).append(genresArray);
            $(elem).append('[' + releaseYear + ']');
            $(elem).attr("href", jd.data[i].url);
        });
    });
});