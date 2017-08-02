$(document).ready(function () {

    console.log('INIT');

    $('.vermais').click(function (event) {
        event.preventDefault();

        var e = $('.vermais');

        var page = $(e).data("page");
        var categoryId = $(e).data("category");
        console.log('get page ' + page);

        $.ajax({
            url: "/umbraco/api/Search/GetFeed/" + page + "?cid=" + categoryId,
            type: 'GET',
            success: function (results) {
                jQuery.each(results, function (i, item) {
                    console.log(item.NodeId);

                    if (item.NodeTypeAlias == 'Article') {
                        $("#feed").append(CreateArticle(item));
                    }
                    else if (item.NodeTypeAlias == 'Note') {
                        $("#feed").append(CreateNote(item));
                    }
                });

                $(e).data("page", page + 1);
                console.log($(e).data("page"));
            }
        });
    });
    function CreateArticle(article) {
        console.log(article);
        var d = document.createElement("div");
        d.className = "post-preview";

        var a = document.createElement("a");
        a.setAttribute('href', article.Url);

        var h2 = document.createElement("h2");
        h2.className = 'post-title';
        h2.innerHTML = article.Title;

        var h3 = document.createElement("h3");
        h3.className = 'post-subtitle';
        h3.innerHTML = article.Subtitle;

        var img = document.createElement("img");
        img.setAttribute("src", article.ThumbnailImageUrl);
        img.style.maxWidth = '100%';

        var video = document.createElement('iframe');
        video.style.maxWidth = '100%';
        video.style.width = '100%';
        video.style.height = '300px';
        video.setAttribute('frameborder', 0);
        video.setAttribute('allowfullscreen', 0);
        video.setAttribute('src', 'https://www.youtube.com/embed/' + article.ThumbnailVideoUrl);

        var p = document.createElement('p');
        p.className = 'post-meta';
        p.style.alignSelf = 'center';
        p.style.paddingTop = '20px';

        var pa = document.createElement('a');
        pa.setAttribute('href', article.ParentUrl);
        pa.innerHTML = article.ParentName;

        var dateSpan = document.createElement('span');
        dateSpan.innerHTML = article.DateFormatted;
        dateSpan.style.paddingLeft = '10px';

        var hr = document.createElement('hr');

        a.appendChild(h2);
        a.appendChild(h3);
        d.appendChild(a);

        if (article.ThumbnailImageUrl != null) {
            d.appendChild(img);
        }
        if (article.ThumbnailVideoUrl != null) {
            console.log("add video::" + article.ThumbnailVideoUrl);
            d.appendChild(video);
        }

        p.appendChild(pa);
        p.appendChild(dateSpan);
        d.appendChild(p);
        d.appendChild(hr);
        return d;
    };

    function CreateNote(article) {
        var d = document.createElement("div");
        d.className = "post-preview text-center";
        d.style.paddingTop = "8px";

        var h2 = document.createElement("h2");
        h2.className = 'post-title';
        h2.style.font = "200 50px/0.8 'Great Vibes', Helvetica, sans-serif";
        h2.style.color = "#deb887";
        h2.innerHTML = article.Title;

        var span = document.createElement('span');
        span.innerHTML = article.Description;

        var p = document.createElement('p');
        p.className = 'post-meta';
        p.style.alignSelf = 'center';
        p.style.paddingTop = '20px';

        var pa = document.createElement('a');
        pa.setAttribute('href', article.ParentUrl);
        pa.innerHTML = article.ParentName;

        var dateSpan = document.createElement('span');
        dateSpan.innerHTML = article.DateFormatted;
        dateSpan.style.paddingLeft = '10px';

        var hr = document.createElement('hr');

        d.appendChild(h2);
        d.appendChild(span);
        p.appendChild(pa);
        p.appendChild(dateSpan);
        d.appendChild(p);
        d.appendChild(hr);
        return d;
    };
});