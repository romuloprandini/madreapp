<?php

namespace App;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\SoftDeletes;
use Illuminate\Auth\Authenticatable;

use Laravel\Passport\HasApiTokens;

class Usuario extends Model
{
    use SoftDeletes, HasApiTokens, Authenticatable;

    protected $token = "token";
    /**
     * The attributes that are mass assignable.
     *
     * @var array
     */
    protected $fillable = [
        'nome', 'email', 'telefone', 'cpf', 'password', 'nascimento', 'avatar'
    ];

    /**
     * The attributes that should be hidden for arrays.
     *
     * @var array
     */
    protected $hidden = [
        'password', 'remember_token', 'deleted_at', 'updated_at', 'created_at', 'avatar'
    ];

    protected $appends = ['avatar_url', 'avatar_thumbnail_url'];


    public function setPasswordAttribute($value) {
        $this->attributes['password'] = bcrypt($value);
    }

    public function getAvatarUrlAttribute()
    {
        if(isset($this->attributes['avatar']) && $this->attributes['avatar'] != "")
        {
            return asset("/assets/images/avatar/".$this->attributes['avatar']);
        }
        return "";
    }

    public function getAvatarThumbnailUrlAttribute()
    {
        if(isset($this->attributes['avatar']) && $this->attributes['avatar'] != "")
        {
            return asset("/assets/images/avatar/thumbnail/".$this->attributes['avatar']);
        }
        return "";
    }
}

