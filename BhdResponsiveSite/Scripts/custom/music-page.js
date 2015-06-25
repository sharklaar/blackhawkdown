var Video = function(title, youtubeId, autoplay) {
    var self = this;
    self.title = title;
    self.youtubeId = youtubeId;
    self.isCurrent = ko.observable(false);
    self.autoplay = ko.observable(autoplay);
    self.image = "http://img.youtube.com/vi/" + self.youtubeId + "/1.jpg";
    self.embedCode = ko.computed(function() {
        return "http://www.youtube.com/embed/" + self.youtubeId + "?info=0&autoplay=" + self.autoplay();
    });
};

var initialVideos = [
        new Video("Simplify", "9MbQ_i9P9aU", + "0"),
        new Video("Poison In My Blood", "g_66lMURxac", + "0"),
        new Video("Freeman - Live", "mf5vExXL1vk", + "0")
];


var VideoModel = function (videos) {
    var self = this;

    self.videos = videos;

    self.playVideo = function(video) {
        video.autoplay("1");
        self.setCurrentVideo(video);
        _trackEvent('Video Thumbnails', 'Select', self.currentVideo.title);
    };

    self.setCurrentVideo = function(video) {
        for (var i = 0; i < videos.length; i++) {
            videos[i].isCurrent(false);
        }

        video.isCurrent(true);
        self.currentVideo(video);
    };

    self.currentVideo = ko.observable();

    self.init = function() {
        self.setCurrentVideo(self.videos[0]);
    };

    self.init();
};

$(document).ready(function () {
    ko.applyBindings(new VideoModel(initialVideos));
});
