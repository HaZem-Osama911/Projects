@extends('layouts.app')
@section('title', 'تسجيل الدخول')
@push('styles')
<link rel="stylesheet" href="{{ asset('assets/css/login.css') }}">
@endpush
@section('content')
<section class="sec1">
    <div class="container">
        <div class="form-box Login">
            <h1>تسجيل الدخول</h1>
            <form action="{{ route('login.post') }}" method="post">
                @csrf
                <div class="input-box">
                    <input name="email" type="email" value="{{ old('email') }}" required autocomplete="email" placeholder="البريد الإلكتروني">
                    <i class="fa-solid fa-envelope"></i>
                </div>
                <div class="input-box">
                    <input name="password" type="password" required autocomplete="current-password" placeholder="كلمة المرور">
                    <i class="fa-solid fa-lock"></i>
                </div>
                <div class="remember-forget">
                    <label><input type="checkbox" name="remember" value="1"> تذكرني</label>
                </div>
                <div class="input-box">
                    <button class="btn" type="submit">تسجيل الدخول</button>
                </div>
                <div class="regi-link">
                    <p>ليس لديك حساب؟ <a href="{{ route('signup') }}" class="signuplink">إنشاء حساب</a></p>
                </div>
            </form>
        </div>
    </div>
</section>
@endsection
