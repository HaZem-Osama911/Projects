@extends('layouts.app')
@section('title', 'فيديوهات الوهن الصوتي')
@push('styles')
<link rel="stylesheet" href="{{ asset('assets/css/videos.css') }}">
@endpush

@section('content')
<section>
        <div class="main-title">الوهن الصوتي<span></span></div>
        <div class="containeer">
            <div class="main-video">
                <div class="video">
                    <video src="{{ asset('assets/video/التنفس.mp4') }}" controls muted autoplay></video>
                    <h3 class="title">01. التمرين الأول -التنفس والتحكم في النفس </h3>
                </div>
            </div>
            <div class="video-list">
                <div class="vid">
                    <video src="{{ asset('assets/video/تدليك الفم من الخارج.mp4') }}" controls muted></video>
                    <h3 class="title">01. التمرين الأول -تدليك الفم من الخارج </h3>
                </div>
                <div class="vid">
                    <video src="{{ asset('assets/video/تدليك الفم من الداخل.mp4') }}" controls muted></video>
                    <h3 class="title">02.التمرين الثاني- تدليك الفم من الداخل</h3>
                </div>
                <div class="vid">
                    <video src="{{ asset('assets/video/تدريب اللسان خارج الفم.mp4') }}" controls muted></video>
                    <h3 class="title">03. التمرين الثالث- اللسان خارج الفم</h3>
                </div>
                <div class="vid">
                    <video src="{{ asset('assets/video/الشفاه.mp4') }}" controls muted></video>
                    <h3 class="title">04. التمرين الرابع -الشفاه </h3>
                </div>
                <div class="vid">
                    <video src="{{ asset('assets/video/فتح وض الشفايف.mp4') }}" controls muted></video>
                    <h3 class="title">05. التمرين الخامس -فتح وضم الشفاه </h3>
                </div>
            </div>
    </section>
    <script>
        let listVideo = document.querySelectorAll('.video-list .vid');
        let mainVideo = document.querySelector('.main-video video');
        let title = document.querySelector('.main-video .title')
        listVideo.forEach(video => {
            video.onclick = () => {
                listVideo.forEach(vid => vid.classList.remove('active'));
                video.classList.add('active');
                if (video.classList.contains('active')) {
                    let src = video.children[0].getAttribute('src');
                    mainVideo.src = src;
                    let text = video.children[1].textContent;
                    title.textContent = text;
                };
            };
        });
    </script>
@endsection
