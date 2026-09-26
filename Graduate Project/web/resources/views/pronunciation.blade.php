@extends('layouts.app')
@section('title', 'تدريب النطق')

@section('content')
<section class="pronunciation-selection">
    <div class="container">
        <div class="main-title">تدريب نطق الحروف <span></span></div>
        <p>اختر اللغة لفتح أداة تسجيل الصوت وتحليل النطق.</p>
        <div class="language-options">
            <a class="cta-button" href="{{ rtrim($arabicUrl, '/') }}/index.html">الحروف العربية</a>
            <a class="cta-button" href="{{ rtrim($englishUrl, '/') }}/index.html">English Letters</a>
        </div>
        <p class="medical-disclaimer">
            هذه الأداة تدريبية ولا تُعد تشخيصًا طبيًا أو بديلًا عن أخصائي التخاطب.
        </p>
    </div>
</section>
@endsection
