@extends('layouts.app')
@section('title', 'فيديوهات البحة')
@push('styles')
<link rel="stylesheet" href="{{ asset('assets/css/videos.css') }}">
@endpush

@section('content')
<section>
        <div class="main-title">البحه<span></span></div>
        <div class="container">
            <div class="containeer">
            <div class="main-video">
                <div class="video">
                    <video src="{{ asset('assets/video/البحه.mp4') }}" controls muted autoplay></video>
                    <h3 class="title">01.التمرين الاول- البحه</h3>
                </div>
            </div>
            <div class="video-list">
                <div class="video-list">
                    <div class="vid">
                        <video src="{{ asset('assets/video/البحه.mp4') }}" controls muted autoplay></video>
                        <h3 class="title">01.التمرين الاول- البحه</h3>
                    </div>
                    <div class="vid">
                        <video src="{{ asset('assets/video/التنفس البطني.mp4') }}" controls muted></video>
                        <h3 class="title">02.التمرين الثاني- تمرين التنفس البطني (تنفس الحجاب الحاجز)</h3>
                    </div>
                </div>
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
