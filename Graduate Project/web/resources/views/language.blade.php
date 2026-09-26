@extends('layouts.app')
@section('title', 'الفيديوهات العلاجيه')
@push('styles')
<link rel="stylesheet" href="{{ asset('assets/css/diagnoses_all.css') }}">
@endpush

@section('content')
<section class="sec2" id="speech-disorders">
    <div class="speechpart">
        <div class="diagnoses-content">
            <div class="container">
                <div class="main-title">اضطرابات الكلام <span></span></div>
                <div class="grid">
                    <div class="card">
                        <h3>اللجلجة</h3>
                        <a href="{{ route('diagnose.stuttering') }}"> الفيديوهات العلاجيه</a>
                    </div>
                    <div class="card">
                        <h3>الخنف</h3>
                        <a href="{{ route('diagnose.nasality') }}"> الفيديوهات العلاجيه</a>
                    </div>
                    <div class="card">
                        <h3>اللثغه</h3>
                        <a href="{{ route('diagnose.lisping') }}"> الفيديوهات العلاجيه</a>
                    </div>
                    <div class="card">
                        <h3>السرعه الزائده في الكلام</h3>
                        <a href="{{ route('diagnose.fast_speech') }}"> الفيديوهات العلاجيه</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</section>

<section id="language-disorders">
    <div class="speechpart">
        <div class="diagnoses-content">
            <div class="container">
                <div class="main-title">اضطرابات اللغه <span></span></div>
                <div class="grid">
                    <div class="card">
                        <h3>تأخر الكلام</h3>
                        <a href="{{ route('diagnose.delay') }}"> الفيديوهات العلاجيه</a>
                    </div>
                    <div class="card">
                        <h3>الحبسه</h3>
                        <a href="{{ route('diagnose.aphasia') }}"> الفيديوهات العلاجيه</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</section>

<section id="voice-disorders">
    <div class="speechpart">
        <div class="diagnoses-content">
            <div class="container">
                <div class="main-title">اضطرابات الصوت <span></span></div>
                <div class="grid">
                    <div class="card">
                        <h3>البحه</h3>
                        <a href="{{ route('diagnose.hoarseness') }}"> الفيديوهات العلاجيه</a>
                    </div>
                    <div class="card">
                        <h3>الوهن الصوتي</h3>
                        <a href="{{ route('diagnose.vocal_asthenia') }}"> الفيديوهات العلاجيه</a>
                    </div>
                    <div class="card">
                        <h3>الحبسه الهستيريه</h3>
                        <a href="{{ route('diagnose.hysterical_aphasia') }}"> الفيديوهات العلاجيه</a>
                    </div>
                    <div class="card">
                        <h3>الخرص الهستيري</h3>
                        <a href="{{ route('diagnose.mutism') }}"> الفيديوهات العلاجيه</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</section>

<section id="autism-disorders">
    <div class="speechpart">
        <div class="diagnoses-content">
            <div class="container">
                <div class="main-title">اضطراب طيف التوحد <span></span></div>
                <div class="grid">
                    <div class="card">
                        <h3>التوحد</h3>
                        <a href="{{ route('diagnose.autism') }}"> الفيديوهات العلاجيه</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</section>
    
</section>
@endsection
