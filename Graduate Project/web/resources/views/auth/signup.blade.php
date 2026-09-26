@extends('layouts.app')
@section('title', 'إنشاء حساب')
@push('styles')
<link rel="stylesheet" href="{{ asset('assets/css/login.css') }}">
@endpush
@section('content')
<section class="sec1">
    <div class="container">
        <div class="form-box Login">
            <h1>إنشاء حساب</h1>
            <form action="{{ route('signup.post') }}" method="post" id="signup-form">
                @csrf
                <div class="input-box">
                    <input type="text" name="username" value="{{ old('username') }}" required maxlength="50" autocomplete="name" placeholder="اسم المستخدم">
                    <i class="fa-solid fa-user"></i>
                </div>
                <div class="input-box">
                    <input type="email" name="email" value="{{ old('email') }}" required autocomplete="email" placeholder="البريد الإلكتروني">
                    <i class="fa-solid fa-envelope"></i>
                </div>
                <div class="input-box">
                    <input type="password" name="password" required minlength="8" autocomplete="new-password" placeholder="كلمة المرور (8 أحرف على الأقل)">
                    <i class="fa-solid fa-lock"></i>
                </div>
                <div class="input-box">
                    <input type="password" name="password_confirmation" required minlength="8" autocomplete="new-password" placeholder="تأكيد كلمة المرور">
                    <i class="fa-solid fa-lock"></i>
                </div>
                <div class="input-box">
                    <input type="tel" name="mobile" value="{{ old('mobile') }}" required maxlength="16" autocomplete="tel" placeholder="رقم الهاتف">
                    <i class="fa-solid fa-phone"></i>
                </div>
                <div class="input-box">
                    <button class="btn" type="submit">إنشاء حساب</button>
                </div>

                <div class="regi-link">
                    <p>لديك حساب بالفعل؟ <a href="{{ route('login') }}" class="loginlink">تسجيل الدخول</a></p>
                </div>
            </form>
        </div>
    </div>
</section>
@endsection
