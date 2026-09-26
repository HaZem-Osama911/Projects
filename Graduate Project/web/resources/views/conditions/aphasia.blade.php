@extends('layouts.app')
@section('title', 'فيديوهات الحبسه')
@push('styles')
<link rel="stylesheet" href="{{ asset('assets/css/videos.css') }}">
@endpush

@section('content')
<section>
        <div class="main-title">الحبسه<span></span></div>
        <div class="containeer">
            <div class="main-video">
                <div class="video">
                    <video src="{{ asset('assets/video/الحبسة الكلامية عند الأطفال Childhood Apraxia of Speech Using Kaufman.mp4') }}"
                        controls muted autoplay></video>
                    <h3 class="title">01. التمرين الأول -الحبسه الكلاميه </h3>
                </div>
            </div>
            <div class="video-list">
                <div class="video-list">

                    <div class="vid">
                        <video src="{{ asset('assets/video/الحبسة الكلامية عند الأطفال Childhood Apraxia of Speech Using Kaufman.mp4') }}"
                            controls muted autoplay></video>
                        <h3 class="title">01. التمرين الأول -الحبسه الكلاميه </h3>
                    </div>
                    <div class="vid">
                        <video src="{{ asset('assets/video/بعض تدريبات جهاز النطق مع الحبسة الكلامية.mp4') }}" controls muted></video>
                        <h3 class="title">02.التمرين الثاني - جهاز النطق مع الحبسة الكلامية</h3>
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
