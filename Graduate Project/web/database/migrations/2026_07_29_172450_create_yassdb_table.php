<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

return new class extends Migration
{
    public function up(): void
    {
        Schema::create('yassdb', function (Blueprint $table) {
            $table->id();
            $table->string('username', 50);
            $table->string('email', 255)->unique();
            $table->string('password');
            $table->string('Mobile', 16);
            $table->rememberToken();
            $table->timestamps();
        });
    }

    public function down(): void
    {
        Schema::dropIfExists('yassdb');
    }
};
