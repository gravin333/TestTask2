using UnityEngine;

namespace CodeBase.Character
{
    public class CharacterMove : MonoBehaviour
    {
        public float speed = 10;
        public float MaxSpeed = 20;

        public LayerMask LayerMask;
        [Range(0, 2)] [SerializeField] private float DistanceToGround = 1f;
        private Animator _animator;
        private CharacterController _characterController;
        private Rigidbody _rb;
        private bool canMove;
        private float ikLeft;
        private float ikRight;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _characterController = GetComponent<CharacterController>();
            _animator = GetComponent<Animator>();
        }


        private void Update()
        {
            if (canMove)
            {
                _animator.SetFloat("Speed", speed / MaxSpeed);
                _characterController.SimpleMove(Vector3.forward * speed * Time.deltaTime);
            }
            else
            {
                //_characterController.
                _animator.SetFloat("Speed", 0);
            }
        }

        private void OnAnimatorIK(int layerIndex)
        {
            if (_animator)
            {
                ikLeft = _animator.GetFloat("IKLeftFootWeight");
                _animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, ikLeft);
                _animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, ikLeft);

                ikRight = _animator.GetFloat("IKRightFootWeight");
                _animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, ikRight);
                _animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, ikRight);

                LeftFoot();
                RightFoot();
            }
        }

        public void StartStop()
        {
            if (canMove)
                canMove = false;
            else
                canMove = true;
        }

        private void LeftFoot()
        {
            var hit = GetRayHit(AvatarIKGoal.LeftFoot);
            var footPosition = hit.point;
            footPosition.y += DistanceToGround;
            SetIKPositionRotation(AvatarIKGoal.LeftFoot, footPosition, hit);
        }

        private void RightFoot()
        {
            var hit = GetRayHit(AvatarIKGoal.RightFoot);
            var footPosition = hit.point;
            footPosition.y += DistanceToGround;
            SetIKPositionRotation(AvatarIKGoal.RightFoot, footPosition, hit);
        }

        private RaycastHit GetRayHit(AvatarIKGoal avatarIKGoal)
        {
            RaycastHit hit;
            var ray = new Ray(_animator.GetIKPosition(avatarIKGoal) + Vector3.up / 2, Vector3.down);
            Physics.Raycast(ray, out hit, DistanceToGround + 1f, LayerMask);
            return hit;
        }

        private void SetIKPositionRotation(AvatarIKGoal avatarIKGoal, Vector3 footPosition, RaycastHit hit)
        {
            _animator.SetIKPosition(avatarIKGoal, footPosition);
            _animator.SetIKRotation(avatarIKGoal, Quaternion.FromToRotation(Vector3.up, hit.normal));
        }
    }
}