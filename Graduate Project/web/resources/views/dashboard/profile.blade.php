@extends('layouts.app')
@section('title', 'الملف الشخصي')
@push('styles')
<link rel="stylesheet" href="{{ asset('assets/css/profile.css') }}">
@endpush

@section('content')
<section class="profile-section">
    <div class="container">
        <div class="profile-box">
            <h1>الملف الشخصي</h1>
            <div class="profile-info">
                <div class="profile-image">
                    <img src="{{ asset('assets/image/user.png') }}" alt="صورة المستخدم">
                </div>
                <form class="user-details" action="{{ route('profile.update') }}" method="POST">
                    @csrf
                    @method('PUT')
                    <div class="detail-item">
                        <label class="label" for="username">اسم المستخدم:</label>
                        <input id="username" name="username" value="{{ old('username', $user->username) }}" required maxlength="50">
                    </div>
                    <div class="detail-item">
                        <label class="label" for="email">البريد الإلكتروني:</label>
                        <input id="email" type="email" name="email" value="{{ old('email', $user->email) }}" required>
                    </div>
                    <div class="detail-item">
                        <label class="label" for="mobile">رقم الهاتف:</label>
                        <input id="mobile" type="tel" name="mobile" value="{{ old('mobile', $user->Mobile) }}" required maxlength="16">
                    </div>
                    <div class="actions">
                        <button type="submit" class="btn edit-btn">حفظ التعديلات</button>
                        <button type="submit" form="logout-form" class="btn logout-btn">تسجيل الخروج</button>
                    </div>
                </form>
            </div>
        </div>
    </div>
</section>
@endsection
