<?php

namespace App;

use Illuminate\Database\Eloquent\Model;

class Ligacao extends Model
{
	protected $table = 'ligacoes';

    /**
     * The attributes that are mass assignable.
     *
     * @var array
     */
	 protected $hidden = ['madrecard_numero', 'madrecard_ativo'];
	 
	 protected $appends = ['madrecard'];
	 
	 public function getMadrecardAttribute()
    {
		if($this->madrecard_numero != null && $this->madrecard_numero != '') {
			return [
				'numero' => $this->madrecard_numero,
				'ativo' => $this->madrecard_ativo ? true : false
			];
		}
		return null;
		
	}
}
