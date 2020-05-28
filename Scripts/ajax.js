$(document).ready(function () {
    $("#sel").change(function () {
        $.ajax({
            type: "POST",
            url: "/Home/GetData",
            data: {
                Genre: $("#sel option:selected").text()
            },
            success: function (movies) {
                // creating JS object from JSON object
                var moviesByGenre = JSON.parse(movies);
                // output string intialization in table format
                var output = "<table class='table table-bordered table-striped'><tr><th>Id</th><th>Title</th><th>Genre</th><th>Duration</th><th>Release Year</th><th>Rating</th></tr>";

                // main logic
                for (idx = 0; idx < moviesByGenre.movies.length; idx++) {
                    output += "<tr><td>" + moviesByGenre.movies[idx].Id + "</td>";
                    output += "<td>" + moviesByGenre.movies[idx].Title + "</td>"
                    output += "<td>" + moviesByGenre.movies[idx].Genre + "</td>"
                    output += "<td>" + moviesByGenre.movies[idx].Duration + "</td>"
                    output += "<td>" + moviesByGenre.movies[idx].Release_Year + "</td>"
                    output += "<td>" + moviesByGenre.movies[idx].Rating + "</td></tr>";
                }

                // completeing ouput string
                output += "</table>";

                // replacing result div with output
                document.getElementById("MoviesData").innerHTML = output;
            },
            error: function () {
                alert("FAILED");
            }
        });
    });
});