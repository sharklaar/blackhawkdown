var Video = function (title, youtubeId) {
    var self = this;
    self.title = title;
    self.youtubeId = youtubeId;
    self.isCurrent = ko.observable(false);
    self.image = "http://img.youtube.com/vi/" + self.youtubeId + "/1.jpg";
    self.embedCode = "http://www.youtube.com/embed/" + self.youtubeId + "?info=0";
}

// The view model is an abstract description of the state of the UI, but without any knowledge of the UI technology (HTML)

var initialVideos = [
        new Video("Simplify", "9MbQ_i9P9aU"),
        new Video("Poison In My Blood", "g_66lMURxac"),
        new Video("Vision Up", "1Fd8KAPiZ64")
];


var videoModel = function (videos) {
    var self = this;

    self.videos = videos;

    self.setCurrentVideo = function (video) {
        video.isCurrent(true);
        self.currentVideo(video);
    }

    self.currentVideo = ko.observable()

    self.init = function () {
        self.setCurrentVideo(self.videos[0]);
    }

    self.init();
};

$(document).ready(function () {
    ko.applyBindings(new videoModel(initialVideos));
});
