<?php

use App\Http\Controllers\AuthController;
use App\Http\Controllers\ContactController;
use App\Http\Controllers\ProfileController;
use App\Http\Controllers\PronunciationController;
use Illuminate\Support\Facades\Route;

Route::view('/', 'home')->name('home');
Route::view('/article', 'articles.article')->name('article');
Route::view('/diagnose', 'dashboard.diagnose')->name('diagnose');
Route::view('/treatment-videos', 'language')->name('conditions.index');
Route::view('/exam', 'exams.exam')->name('exam');
Route::get('/pronunciation', PronunciationController::class)->name('pronunciation');

Route::middleware('guest')->group(function () {
    Route::view('/login', 'auth.login')->name('login');

    Route::post('/login', [AuthController::class, 'login'])->name('login.post');

    Route::view('/signup', 'auth.signup')->name('signup');

    Route::post('/signup', [AuthController::class, 'signup'])->name('signup.post');
});

Route::middleware('auth')->group(function () {
    Route::post('/logout', [AuthController::class, 'logout'])->name('logout');
    Route::get('/profile', [ProfileController::class, 'show'])->name('profile');
    Route::put('/profile', [ProfileController::class, 'update'])->name('profile.update');
});

Route::post('/contact', [ContactController::class, 'store'])
    ->middleware('throttle:5,1')
    ->name('contact.post');

// Conditions routes
$conditions = [
    'aphasia', 'autism', 'delay', 'fast_speech', 'hoarseness',
    'hysterical_aphasia', 'lisping', 'mutism', 'nasality', 'stuttering', 'vocal_asthenia',
];

foreach ($conditions as $condition) {
    Route::view("/diagnose/{$condition}", "conditions.{$condition}")
        ->name("diagnose.{$condition}");
}

// Exam routes
$exams = ['exam1', 'exam2', 'exam3', 'exam4', 'guidedexam'];
foreach ($exams as $exam) {
    Route::view("/exam/{$exam}", "exams.{$exam}")
        ->name("exam.{$exam}");
}
