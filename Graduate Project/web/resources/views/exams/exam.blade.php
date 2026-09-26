@extends('layouts.app')
@section('title', 'الاختبارات')
@push('styles')
<link rel="stylesheet" href="{{ asset('assets/css/exam.css') }}">
@endpush

@section('content')
<div class="exercise-content">
        <div class="container">
            <div class="main-title">الاختبارات <span></span></div>
            <p class="description">اختار من بين مجموعة اختبارات دقيقة لمساعدتك على فهم حالة طفلك بشكل أفضل</p>
            <div class="guided-test-box">
                <h4>مش عارف تبدأ منين؟</h4>
                <p>جاوب على ٤ أسئلة بسيطة واحنا هنرشحلك أنسب اختبار لحالة طفلك </p>
                <a href="{{ route('exam.guidedexam') }}" class="guided-link">ابدأ الاختبار</a>
            </div>
            <div class="grid">
                <div class="card intelligence">
                    <h3>اختبار الذكاء</h3>
                    <p>يساعد في تقييم مستوى التفكير والتركيز لدى الطفل</p>
                    <div class="a-container">
                        <a href="{{ route('exam.exam1') }}">ابدأ الاختبار</a>
                    </div>
                </div>
                <div class="card age">
                    <h3>قياس العمر العقلي</h3>
                    <p>يُستخدم لتحديد العمر العقلي الحقيقي للطفل مقارنة بعمره الزمني</p>
                    <div class="a-container">
                        <a href="{{ route('exam.exam2') }}">ابدأ الاختبار</a>
                    </div>
                </div>
                <div class="card language">
                    <h3>اختبار الفهم اللغوي</h3>
                    <p>يُقيّم قدرة الطفل على فهم واستيعاب اللغة</p>
                    <div class="a-container">
                        <a href="{{ route('exam.exam3') }}">ابدأ الاختبار</a>
                    </div>
                </div>
                <div class="card autism">
                    <h3>اختبار M-CHAT</h3>
                    <p>استبيان توعوي أولي لبعض مؤشرات التواصل، وليس تشخيصًا طبيًا</p>
                    <div class="a-container">
                        <a href="{{ route('exam.exam4') }}">ابدأ الاختبار</a>
                    </div>
                </div>
                <div class=" card char">
                    <h3>اختبار نطق الحروف</h3>
                    <p>يُقيّم قدرة الطفل علي نطق الحرف باستخدام AI </p>
                    <div class="a-container">
                        <a href="{{ route('pronunciation') }}">ابدأ الاختبار</a>
                    </div>
                </div>
            </div>
        </div> 
        <br>
        <br>
        <br>
        <br>
        <p class="descriptionn">
            ملحوظه مهمه :الأخصائي هو من يتم تشخيص الحالة وفقا لدراسه الحالة والملاحظة الدقيقة ,ثم يضع خطة فرديه
            مناسبة لكل حالة يحدد المدة المخصصة لكل برنامج و ينتقل من كل
            مرحله لاخري حسب تقدم الحاله<br> ويكون عنده معرفه بالبرامج الخاصه بكل حاله و لابد ان يتمتع الاخصائي بالمرونه
            في تعديل خطوات البرنامج الخاص بالحاله عند ظهور اي سلوك معطل أو تقدم مفاجئ
        </p>
    </div>

    </section>
@endsection
