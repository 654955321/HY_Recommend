using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VayneHunter_Reborn.External.Translation.Languages
{
    /*
     * Korean Translation by Tetrahedrite
     * https://www.joduska.me/forum/user/74465-tetrahedrite/
     */
    class Korean : IVHRLanguage
    {
        public string GetName()
        {
            return "Korean";
        }

        public Dictionary<string, string> GetTranslations()
        {
            var translations = new Dictionary<string, string>
            {
                {"dz191.vhr.combo.useq", "Q ���"},
                {"dz191.vhr.combo.usee", "E ���"},
                {"dz191.vhr.combo.user", "R ���"},
                {"dz191.vhr.combo.mm.q.mana", "Q ����"},
                {"dz191.vhr.combo.mm.e.mana", "E ����"},
                {"dz191.vhr.combo.mm.r.mana", "R ����"},
                {"dz191.vhr.combo.r.minenemies", "�ּ� R ���"},
                {"dz191.vhr.combo.q.2wstacks", "Ÿ�ٿ� W�� 2���� ���� ��쿡�� Q ���"},
                {"dz191.vhr.mixed.useq", "Q ���"},
                {"dz191.vhr.mixed.usee", "E ���"},
                {"dz191.vhr.mixed.mm.q.mana", "Q ����"},
                {"dz191.vhr.mixed.mm.e.mana", "E ����"},
                {"dz191.vhr.mixed.q.2wstacks", "Ÿ�ٿ� W�� 2���� ���� ��쿡�� Q ���"},
                {"dz191.vhr.mixed.ethird", "����° ������ ���� E ���"},
                {"dz191.vhr.laneclear.useq", "Q ��� (Lane Clear)"},
                {"dz191.vhr.laneclear.mm.q.mana", "Q ����"},
                {"dz191.vhr.lasthit.useq", "Q ��� (Last Hit)"},
                {"dz191.vhr.lasthit.mm.q.mana", "Q ����"},
                {"dz191.vhr.farm.condemnjungle", "���� ���� ���� ���� E ���"},
                {"dz191.vhr.farm.qjungle", "���� ���� Q ���"},
                {"dz191.vhr.misc.condemn.qlogic", "Q ��"},
                {"dz191.vhr.mixed.mirinQ", "������ ��� ������ Q (Mirin ���)"},
                {"dz191.vhr.misc.tumble.smartq", "������ ��� QE �õ�"},
                {"dz191.vhr.misc.tumble.noaastealthex", "���� ������ �� ��Ÿ ��� �� ��"},
                {"dz191.vhr.misc.tumble.noqenemies", "��� ���̷� Q ��� �� ��"},
                {"dz191.vhr.misc.tumble.dynamicqsafety", "Q�� �������� ������ �Ÿ��� ���"},
                {"dz191.vhr.misc.tumble.qspam", "Q üũ ����"},
                {"dz191.vhr.misc.tumble.qinrange", "Q ų ��ƿ"},
                {"dz191.vhr.misc.tumble.walltumble.warning", "�� �����⸦ ������ �輼��"},
                {"dz191.vhr.misc.tumble.walltumble.warning.2", "����� �� ������ �������� �̵��ؼ� �����̴ϴ�"},
                {"dz191.vhr.misc.tumble.walltumble", "������� �� �ѱ� (�� ������)"},
                {"dz191.vhr.misc.condemn.condemnmethod", "���� ���"},
                {"dz191.vhr.misc.condemn.pushdistance", "E Ǫ�� �Ÿ�"},
                {"dz191.vhr.misc.condemn.accuracy", "��Ȯ�� (Revolution�� ����)"},
                {"dz191.vhr.misc.condemn.enextauto", "E �ڵ����� ���� ���ݿ� ���"},
                {"dz191.vhr.misc.condemn.onlystuncurrent", "���� Ÿ�ٸ� ����"},
                {"dz191.vhr.misc.condemn.autoe", "�ڵ� E"},
                {"dz191.vhr.misc.condemn.eks", "����Ʈ E ų ��ƿ"},
                {"dz191.vhr.misc.condemn.noeaa", "Ÿ���� ��Ÿ Xȸ �ȿ� ���� ��� E ��� ����"},
                {"dz191.vhr.misc.condemn.trinketbush", "���� �� �νÿ� ��ű� ���"},
                {"dz191.vhr.misc.condemn.lowlifepeel", "ü���� ���� �� E�� �о��"},
                {"dz191.vhr.misc.condemn.condemnflag", "�ڸ��� 4�� ��â�� ����"},
                {"dz191.vhr.misc.condemn.noeturret", "��� ��ž�� E ��� �� ��"},
                {"dz191.vhr.misc.general.antigp", "��Ƽ ��Ŭ����(���� ����)"},
                {"dz191.vhr.misc.general.interrupt", "���ͷ���"},
                {"dz191.vhr.misc.general.antigpdelay", "��Ƽ ��Ŭ���� ������ (ms)"},
                {"dz191.vhr.misc.general.specialfocus", "W 2���� Ÿ���� ��Ŀ��"},
                {"dz191.vhr.misc.general.reveal", "���� ���� (���� ���� �͵� / ����)"},
                {"dz191.vhr.misc.general.disablemovement", "�����Ŀ ������ ��Ȱ��ȭ"},
                {"dz191.vhr.misc.general.disableattk", "�����Ŀ ��Ÿ ��Ȱ��ȭ"},
                {"dz191.vhr.draw.spots", "�� ������ ��ġ �׸���"},
                {"dz191.vhr.draw.range", "��� ���� ���� �׸���"},
                {"dz191.vhr.draw.qpos", "Reborn Q ��ġ (�����)"},
                {"dz191.vhr.activator.onkey", "��Ƽ������ Ű"},
                {"dz191.vhr.activator.always", "�׻� �ѱ�"},
                {"dz191.vhr.activator.spells.barrier.onhealth", "ü�� % ���� �� ��"},
                {"dz191.vhr.activator.spells.barrier.ls", "Evade/������ ������� ����"},
                {"dz191.vhr.activator.spells.heal.onhealth", "ü�� % ���� �� ��"},
                {"dz191.vhr.activator.spells.heal.ls", "Evade/������ ������� ����"},
                {"dz191.vhr.cleanser.bufftype.Blind", "�Ǹ�"},
                {"dz191.vhr.cleanser.bufftype.Charm", "��Ȥ"},
                //{"dz191.vhr.cleanser.bufftype.CombatDehancer", ""},
                {"dz191.vhr.cleanser.bufftype.Fear", "���� (��������)"},
                {"dz191.vhr.cleanser.bufftype.Flee", "���� (�������� ����)"},
                {"dz191.vhr.cleanser.bufftype.Knockback", "��ġ��"},
                {"dz191.vhr.cleanser.bufftype.Knockup", "���"},
                {"dz191.vhr.cleanser.bufftype.Polymorph", "����"},
                {"dz191.vhr.cleanser.bufftype.Silence", "ħ��"},
                {"dz191.vhr.cleanser.bufftype.Sleep", "����(?)"},
                {"dz191.vhr.cleanser.bufftype.Snare", "�ӹ�"},
                {"dz191.vhr.cleanser.bufftype.Stun", "����"},
                {"dz191.vhr.cleanser.bufftype.Suppression", "����"},
                {"dz191.vhr.cleanser.bufftype.Taunt", "����"},
                {"dz191.vhr.cleanser.bufftype.minbuffs", "�ּ� ���� ��"},
                {"dz191.vhr.cleanser.use.combo", "Ŭ���� Ű"},
                {"dz191.vhr.cleanser.use", "�׻� �ѱ�"},
                {"dz191.vhr.ps.profile", "������"},
                {"dz191.vhr.ps.load", "�ҷ�����"},
                {"dz191.vhr.ps.savenew", "�� �����ʷ� ����"},
                {"dz191.vhr.ps.savecurrent", "���� �����ʷ� ����"},
                {"dz191.vhr.ps.showdesc", "���� ����"},
                {"dz191.vhr.ps.delete", "���� ������ ����"},
                {"dz191.vhr.ps.desc1", "ä�ÿ� .rename <�� �̸�>�� �Է��Ͻø� ���� ������ �̸��� ����˴ϴ�."},
                {"dz191.vhr.translations.language", "���"},
                {"dz191.vhr.translations.select", "�����ϱ�!"},
            };

            return translations;
        }
    }
}

